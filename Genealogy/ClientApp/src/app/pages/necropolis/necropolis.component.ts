import { Component, OnInit, ViewChild } from '@angular/core';
import { Store } from '@ngxs/store';
import { Validators, FormControl, FormGroup } from '@angular/forms';
import { Cemetery, CemeteryFilter, Paginator, Person, PersonFilter, Table } from '@models';
import { ClearPersonList, FetchActiveSubscribe, FetchCemeteryList, FetchCountyList, FetchPersonList, GetCemeteries, GetCemetery } from '@actions';
import { CemeteryState, CountyState, MainState, PersonState } from '@states';
import { isNil, isEmpty, difference } from 'lodash';
import { NotifierService } from 'angular-notifier';
import { NOTIFICATIONS } from '@enums';
import { FeedbackComponent } from '@shared';
import { switchMap, switchMapTo, tap } from 'rxjs/operators';
import { defer, iif, of } from 'rxjs';

@Component({
  selector: 'app-necropolis',
  templateUrl: './necropolis.component.html',
  styleUrls: ['./necropolis.component.scss'],
})
export class NecropolisComponent implements OnInit {
  @ViewChild(FeedbackComponent, { static: false }) feedbackModal: FeedbackComponent;

  personList: Array<Person> = null;
  searchForm: FormGroup;

  hasAuth = false;
  hasSubscription: boolean = null;

  tableData: Table.Data;
  paginatorOptions: Paginator = {
    index: 0,
    step: 10,
    count: 0,
  };

  counties: Array<{ id: string; name: string }>;
  cemeteries: Array<{ id: string; name: string }>;

  countyId?: string = null

  constructor(private store: Store, private notifierService: NotifierService) {}

  ngOnInit() {
    this.hasAuth = this.store.selectSnapshot(MainState.hasAuth);

    this.searchForm = new FormGroup({
      fio: new FormControl(null, [Validators.required]),
      countyId: new FormControl(null, [Validators.required]),
      cemeteryId: new FormControl(null, [Validators.required],)
    });

    if (this.hasAuth) {
      this.store.dispatch(new FetchActiveSubscribe()).pipe(
        tap(() => this.hasSubscription = true),
        //tap(() => this.hasSubscription = this.store.selectSnapshot(MainState.hasSubscription)),
        switchMap(() =>
          iif(
            () => this.hasSubscription,
            defer(() => this.store.dispatch(new FetchCountyList())),
            defer(() => of({}))
          )
        )).subscribe(() => {
          const countyList = this.store.selectSnapshot(CountyState.countyList);
          this.counties = countyList.map(({ id, name }) => ({ id, name }));
        }
      );
    }

    this.store.dispatch(new ClearPersonList());
  }

  onSearch() {
    this.search();
  }

  onOrderButtonClick() {
    this.feedbackModal.open('Некрополистическое исследование');
  }

  onChangeCounty() {
    console.log(this.searchForm)
    this.countyId = this.searchForm.value['countyId']
    this.getCemeteries()
  }

  private search() {
    const { fio, countyId, cemeteryId } = this.searchForm.value;

    if (!isEmpty(fio) && !isNil(cemeteryId)) {
      const { index, step } = this.paginatorOptions;
      const filter: PersonFilter = { fio: fio.toLowerCase(), cemeteryId, index, step };

      this.store.dispatch(new FetchPersonList(filter)).subscribe(() => {
        this.personList = this.store.selectSnapshot<Array<Person>>(PersonState.personList);

        this.paginatorOptions.count = this.personList.length;

        const items = this.convertToItems(this.personList);

        const persons = (this.tableData = {
          fields: ['ФИО', 'Дата рождения', 'Дата смерти'],
          items,
        });
      });
    } else {
      this.notifierService.notify('error', NOTIFICATIONS.INVALID_FORM, 'INVALID_FORM');
    }
  }

  private convertToItems(persons: Person[], isChild: boolean = false) {
    if (isEmpty(persons)) {
      return [];
    }
    let addedChildIds = new Array<string>();

    persons.forEach(person => {
      if (!isNil(person.personGroup) && !isChild) {
      }
    });

    return persons
      .map<Table.Item>(person => {
        let childs: Person[];

        if (addedChildIds.includes(person.id)) {
          return null;
        }

        if (!isNil(person.personGroup) && !isChild) {
          childs = persons.filter(p => p.id !== person.id && p.personGroup && p.personGroup.id === person.personGroup.id);
          childs.forEach(c => addedChildIds.push(c.id));
        }

        const firstname = !isEmpty(person.firstname) ? person.firstname : '';
        const lastname = !isEmpty(person.lastname) ? person.lastname : '';
        const patronymic = !isEmpty(person.patronymic) ? person.patronymic : '';
        return {
          id: person.id,
          values: [`${lastname} ${firstname}  ${patronymic}`, person.startDate, person.finishDate],
          isRemoved: person.isRemoved,
          childs: this.convertToItems(childs, true),
        };
      })
      .filter(item => !isNil(item));
  }

  private getCemeteries() {
    const filter: CemeteryFilter = { countyId: this.countyId}
    this.store.dispatch(new GetCemeteries(filter)).subscribe(() => {
      this.cemeteries = this.store.selectSnapshot<Array<Cemetery>>(CemeteryState.cemeteryList)
    })

  }
}
