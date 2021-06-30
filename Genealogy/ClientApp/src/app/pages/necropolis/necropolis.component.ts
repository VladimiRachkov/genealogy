import { Component, OnInit, ViewChild } from '@angular/core';
import { Store } from '@ngxs/store';
import { Validators, FormControl, FormGroup } from '@angular/forms';
import { Paginator, Person, PersonFilter, Table } from '@models';
import { ClearPersonList, FetchActiveSubscribe, FetchCemeteryList, FetchPersonList } from '@actions';
import { CemeteryState, MainState, PersonState } from '@states';
import { isNil, isEmpty, difference } from 'lodash';
import { NotifierService } from 'angular-notifier';
import { NOTIFICATIONS } from '@enums';
import { FeedbackComponent } from '@shared';

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

  cemeteries: Array<{ id: string; name: string }>;

  constructor(private store: Store, private notifierService: NotifierService) {}

  ngOnInit() {
    this.hasAuth = this.store.selectSnapshot(MainState.hasAuth);

    this.searchForm = new FormGroup({
      fio: new FormControl(null, [Validators.required]),
      cemeteryId: new FormControl(null, [Validators.required]),
    });

    if (this.hasAuth) {
      this.store.dispatch(new FetchCemeteryList()).subscribe(() => {
        const cemeteryList = this.store.selectSnapshot(CemeteryState.cemeteryList);
        this.cemeteries = cemeteryList.map(({ id, name }) => ({ id, name }));
      });

      this.store.dispatch(new FetchActiveSubscribe()).subscribe(() => {
        this.hasSubscription = this.store.selectSnapshot(MainState.hasSubscription);
      });
    }

    this.store.dispatch(new ClearPersonList());
  }

  onSearch() {
    this.search();
  }

  onOrderButtonClick() {
    this.feedbackModal.open('Некрополистическое исследование');
  }

  private search() {
    const { fio, cemeteryId } = this.searchForm.value;

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
}
