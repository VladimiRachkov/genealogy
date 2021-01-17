import { Component, OnInit, OnDestroy } from '@angular/core';
import { Store, Select } from '@ngxs/store';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { Table, Person, Cemetery, PersonDto, PersonFilter, Paginator } from '@models';
import { CemeteryState, PersonState } from '@states';
import { AddPerson, GetPerson, MarkAsRemovedPerson, UpdatePerson, FetchPersonList, GetPersonsCount } from '@actions';
import { untilDestroyed } from 'ngx-take-until-destroy';

@Component({
  selector: 'dashboard-person',
  templateUrl: './person.component.html',
  styleUrls: ['./person.component.scss'],
})
export class PersonComponent implements OnInit, OnDestroy {
  tableData: Table.Data;
  personList: Array<Person>;
  personForm: FormGroup;
  person: Person;

  paginatorOptions: Paginator;

  @Select(CemeteryState.cemeteryList) cemeteryList$: Observable<Array<Cemetery>>;

  constructor(private store: Store) {}

  ngOnInit() {
    this.store.dispatch(new GetPersonsCount()).subscribe(data => {
      const { count } = data.person;
      console.log('COUNT', count);

      this.paginatorOptions = {
        index: 0,
        step: 10,
        count,
      };

      this.updateList();
    });

    this.personForm = new FormGroup({
      id: new FormControl(null),
      lastname: new FormControl(null, [Validators.required]),
      firstname: new FormControl(null, [Validators.required]),
      patronymic: new FormControl(null, [Validators.required]),
      startDate: new FormControl(null, [Validators.required]),
      finishDate: new FormControl(null, [Validators.required]),
      cemeteryId: new FormControl(null, null),
      source: new FormControl(null, null),
      comment: new FormControl(null, null),
    });
    this.person = null;
    /// this.personForm.valueChanges.pipe(untilDestroyed(this)).subscribe(data => console.log(data));
  }

  ngOnDestroy() {}

  onReset() {
    this.personForm.getRawValue();
  }

  onAdd() {
    const person = this.personForm.value as PersonDto;

    this.personForm.value.cemeteryId;
    this.store.dispatch(new AddPerson(person)).subscribe(() => this.updateList());
  }

  onSelect(id: string) {
    console.log('ID', id)
    const filter: PersonFilter = { id };

    this.store.dispatch(new GetPerson(filter)).subscribe(() => {
      this.personForm.reset();

      const person = this.store.selectSnapshot<PersonDto>(PersonState.person);
      const { id, lastname, firstname, patronymic, cemeteryId, startDate, finishDate, source, comment } = person;

      this.person = person as Person;
      this.personForm.setValue({ id, lastname, firstname, patronymic, cemeteryId, startDate, finishDate, source, comment });
    });
  }

  onRemove(id: string) {
    this.store.dispatch(new MarkAsRemovedPerson(id)).subscribe(() => this.updateList());
  }

  onUpdate() {
    const person: PersonDto = this.personForm.value;
    this.store.dispatch(new UpdatePerson(person)).subscribe(() => this.updateList());
  }

  private updateList(pageIndex: number = 0) {
    const { step } = this.paginatorOptions;
    this.store.dispatch(new FetchPersonList({ index: pageIndex, step })).subscribe(() => {
      this.personList = this.store.selectSnapshot<Array<Person>>(PersonState.personList);

      const items = this.personList.map<Table.Item>(item => ({
        id: item.id,
        values: [
          `${item.lastname} ${item.firstname} ${item.patronymic}`,
          item.startDate,
          item.finishDate,
          item.cemetery ? item.cemetery.name : null,
        ],
        isHidden: item.isRemoved,
      }));

      this.tableData = {
        fields: ['ФИО', 'Дата рождения', 'Дата смерти', 'Место захоронения'],
        items,
      };
      this.personForm.getRawValue();
      this.personForm.updateValueAndValidity();
    });
  }

  onChangePage(pageIndex: number) {
    this.updateList(pageIndex);
  }
}
