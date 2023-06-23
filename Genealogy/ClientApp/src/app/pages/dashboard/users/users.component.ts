import { Component, OnInit, ViewChild } from '@angular/core';
import { User, Table, UserFilter, UserDto, BusinessObject, BusinessObjectFilter, ProductForUserOutDto } from '@models';
import { Store, Select } from '@ngxs/store';
import { AddPurchaseForUser, FetchActiveSubscription, FetchCatalogItem, FetchUserList, GetUser, UpdateUser } from '@actions';
import { CatalogState, MainState, UserState } from '@states';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { METATYPE_ID, USER_STATUS } from '@enums';
import { UserStatusPipe } from 'app/shared/pipes';
import { switchMap, tap } from 'rxjs/operators';
import { NotifierService } from 'angular-notifier';
import { NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'dashboard-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss'],
  providers: [UserStatusPipe],
})
export class UsersComponent implements OnInit {
  @ViewChild('content', { static: false }) content;
  
  @Select(UserState.user) user$: Observable<User>;

  userList: Array<User>;
  tableData: Table.Data;
  userForm: FormGroup;
  user?: User;
  USER_STATUS = USER_STATUS;
  hasUserActived: boolean;
  hasSubscription: boolean;
  startIndex = 0;

  constructor(
    private store: Store, 
    private userStatusPipe: UserStatusPipe, 
    private notifierService: NotifierService, 
    private modalService: NgbModal) {}

  ngOnInit() {
    this.userForm = new FormGroup({
      id: new FormControl(null),
      lastName: new FormControl({ value: null, disabled: true }, [Validators.required]),
      firstName: new FormControl({ value: null, disabled: true }, [Validators.required]),
      email: new FormControl({ value: null, disabled: true }, [Validators.required, Validators.email]),
    });
    this.updateList();
  }

  onSelect(id: string) {
    const filter: UserFilter = { id };
    this.store
      .dispatch(new GetUser(filter))
      .pipe(
        switchMap(() => this.store.dispatch(new FetchActiveSubscription({ id }))),
        tap(() => (this.hasSubscription = this.store.selectSnapshot(MainState.hasSubscription)))
      )
      .subscribe(() => {
        this.user = this.store.selectSnapshot<UserDto>(UserState.user) as User;
        const { id, lastName, firstName, email, status } = this.user;
        this.userForm.setValue({ id, lastName, firstName, email });
        this.hasUserActived = this.user.status === USER_STATUS.ACTIVE || this.user.status === USER_STATUS.PAID;
        this.openUserDetails()
      });
  }

  onReset() {
    this.store.reset(UserState.user);
  }

  private updateList() {
    this.store.dispatch(new FetchUserList({})).subscribe(() => {
      this.userList = this.store.selectSnapshot<Array<User>>(UserState.userList);

      const items = this.userList.map<Table.Item>(item => ({
        id: item.id,
        values: [`${item.lastName} ${item.firstName}`, item.email, this.userStatusPipe.transform(item.status)],
        isRemoved: item.status == USER_STATUS.BLOCKED,
      }));

      this.tableData = {
        fields: ['ФИО', 'Email', 'Статус'],
        items,
      };
    });
  }

  onRemove(id: string) {
    this.store.dispatch(new UpdateUser({ id, status: USER_STATUS.BLOCKED })).subscribe(() => this.updateList());
  }

  onRestore(id: string) {
    this.store.dispatch(new UpdateUser({ id, status: USER_STATUS.ACTIVE })).subscribe(() => this.updateList());
  }

  onAddSubscrible() {
    let filter: BusinessObjectFilter = { name: 'SUBSCRIPTION', metatypeId: METATYPE_ID.PRODUCT };
    this.store
      .dispatch(new FetchCatalogItem(filter))
      .pipe(
        switchMap(() => {
          let productId = this.store.selectSnapshot<BusinessObject>(CatalogState.item).id;
          return this.store.dispatch(new AddPurchaseForUser({ userId: this.user.id, productId: productId }));
        })
      )
      .subscribe(() => {
        let result = this.store.selectSnapshot<boolean>(MainState.addSubscribleFailed);
        if (result) {
          this.notifierService.notify('success', 'Подписка успешно активирована', 'ADD_SUBSCRIBLE_SUCCESS');
          this.hasSubscription = true;
        } else {
          this.notifierService.notify('error', 'Ошибка при активизации подписки', 'ADD_SUBSCRIBLE_FAILED');
        }
      });
  }

  openUserDetails() {
    const options: NgbModalOptions = {
      ariaLabelledBy: 'modal-basic-title',
      size: 'md',
      windowClass: 'your-custom-dialog-class',
    };

    this.modalService.open(this.content, options);
  }
}
