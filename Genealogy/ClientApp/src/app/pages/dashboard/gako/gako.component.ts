import { Component, OnInit } from '@angular/core';
import { Store, Select } from '@ngxs/store';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { Table, Person, Cemetery, PersonDto, PersonFilter } from '@models';
import { CemeteryState, PersonState } from '@states';
import { AddPerson, GetPerson, MarkAsRemovedPerson, UpdatePerson, FetchPersonList } from '@actions';

@Component({
  selector: 'dashboard-gako',
  templateUrl: './gako.component.html',
  styleUrls: ['./gako.component.scss'],
})
export class GakoComponent implements OnInit {
  tableData: Table.Data;
  personList: Array<Person>;
  personForm: FormGroup;
  person: Person;

  @Select(CemeteryState.cemeteryList) cemeteryList$: Observable<Array<Cemetery>>;

  constructor(private store: Store) {}

  ngOnInit() {
    this.updateList();
    this.personForm = new FormGroup({
      id: new FormControl(null),
      lastname: new FormControl(null, [Validators.required]),
      firstname: new FormControl(null, [Validators.required]),
      patronymic: new FormControl(null, [Validators.required]),
      startDate: new FormControl(null, [Validators.required]),
      finishDate: new FormControl(null, [Validators.required]),
      cemeteryId: new FormControl(null, null),
    });
    this.person = null;
    this.personForm.valueChanges.subscribe(data => console.log(data));
  }

  onReset() {
    this.personForm.getRawValue();
  }

  onAdd() {
    const person = this.personForm.value as PersonDto;

    this.personForm.value.cemeteryId;
    this.store.dispatch(new AddPerson(person)).subscribe(() => this.updateList());
  }

  onSelect(id: string) {
    const filter: PersonFilter = { id };
    this.store.dispatch(new GetPerson(filter)).subscribe(() => {
      const person = this.store.selectSnapshot<PersonDto>(PersonState.person);
      const { id, lastname, firstname, patronymic, cemeteryId, startDate, finishDate } = person;
      this.person = person as Person;
      this.personForm.setValue({ id, lastname, firstname, patronymic, cemeteryId, startDate, finishDate });
    });
  }

  onRemove(id: string) {
    this.store.dispatch(new MarkAsRemovedPerson(id)).subscribe(() => this.updateList());
  }

  onUpdate() {
    const person: PersonDto = this.personForm.value;
    this.store.dispatch(new UpdatePerson(person)).subscribe(() => this.updateList());
  }

  private updateList() {
    this.store.dispatch(new FetchPersonList({})).subscribe(() => {
      this.personList = this.store.selectSnapshot<Array<Person>>(PersonState.personList);

      const items = this.personList.map<Table.Item>(item => ({
        id: item.id,
        values: [
          `${item.lastname} ${item.firstname} ${item.patronymic}`,
          item.startDate,
          item.finishDate,
          item.cemetery ? item.cemetery.name : null,
        ],
      }));

      this.tableData = {
        fields: ['ФИО', 'Дата рождения', 'Дата смерти', 'Место захоронения'],
        items,
      };
      this.personForm.getRawValue();
    });
  }
}
