import { FetchSettingList, GetSettingsCount, UpdateSetting } from '@actions';
import { DatePipe } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BusinessObject, BusinessObjectFilter, BusinessObjectOutDto, Paginator, Table } from '@models';
import { Select, Store } from '@ngxs/store';
import { ModalComponent } from '@shared';
import { SettingState } from '@states';
import { MailState } from 'app/states/mail.state';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Component({
  selector: 'dashboard-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.scss'],
  providers: [DatePipe],
})
export class SettingsComponent implements OnInit {
  @ViewChild(ModalComponent, { static: false }) purchaseModal: ModalComponent;

  @Select(SettingState.item)
  item$: Observable<BusinessObject>;

  @Select(SettingState.list)
  list$: Observable<BusinessObject[]>;

  tableData$: Observable<Table.Data>;

  paginatorOptions: Paginator = {
    index: 0,
    step: 10,
    count: null,
  };

  pageIndex: number = 0;
  startIndex: number = 0;

  settingsForm: FormGroup;

  constructor(private store: Store, private datePipe: DatePipe) {}

  ngOnInit() {
    //this.updatePaginator();

    this.settingsForm = new FormGroup({
      id: new FormControl(null),
      data: new FormControl(null, [Validators.required]),
    });

    this.tableData$ = this.list$.pipe(
      map<BusinessObject[], Table.Item[]>(list => list.map(({ id, title, data }) => ({ id, values: [title, data] }))),
      map<Table.Item[], Table.Data>(items => ({
        fields: ['Параметр', 'Значение'],
        items,
      }))
    );
  }

  onSelect(id: string) {}

  onRemove(id: string) {
    this.updateItem({ id, isRemoved: true });
  }

  onRestore(id: string) {
    this.updateItem({ id, isRemoved: false });
  }

  onChangePage(pageIndex: number) {
    const { step } = this.paginatorOptions;

    this.pageIndex = pageIndex;
    this.startIndex = pageIndex * step;
    this.fetchList(pageIndex);
  }

  private updateItem(body: BusinessObjectOutDto) {
    this.store.dispatch(new UpdateSetting(body)).subscribe(() => this.fetchList());
  }

  private fetchList(index: number = this.pageIndex) {
    const { step } = this.paginatorOptions;
    const params: BusinessObjectFilter = { index, step };

    this.paginatorOptions.index = index;

    this.store.dispatch(new FetchSettingList()).subscribe();
  }

  private updatePaginator() {
    const { step } = this.paginatorOptions;

    this.store.dispatch(new GetSettingsCount()).subscribe(() => {
      const count = this.store.selectSnapshot<number>(MailState.count);

      this.paginatorOptions = {
        index: 0,
        step,
        count,
      };

      this.fetchList();
    });
  }
}
