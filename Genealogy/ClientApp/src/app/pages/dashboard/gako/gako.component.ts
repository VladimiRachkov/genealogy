import { Component, OnInit } from '@angular/core';
import { Table } from '@shared/components/dashboard/table/table';
import { Store, Select } from '@ngxs/store';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Person } from '@mdl/person';
import { PersonState } from '@state/person.state';
import { FetchPersonList, AddPerson, MarkAsRemovedPerson, GetPerson, UpdatePerson } from '@act/person.actions';
import { PersonDto } from '@mdl/dtos/person.dto';
import { CemeteryState } from '@state/cemetery.state';
import { Observable } from 'rxjs';
import { Cemetery } from '@mdl/cemetery';
import { PersonFilter } from '@mdl/filters/person.filter';

@Component({
  selector: 'dashboard-gako',
  templateUrl: './gako.component.html',
  styleUrls: ['./gako.component.scss'],
})
export class GakoComponent implements OnInit {
  tableData: Table.Data;
  personList: Array<Person>;
  personForm: FormGroup;
  selectedPerson: Person;

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
    this.selectedPerson = null;
    this.personForm.valueChanges.subscribe(data => console.log(data));
  }

  resetForm() {
    this.personForm.getRawValue();
  }

  addPerson() {
    const person = this.personForm.value as PersonDto;

    this.personForm.value.cemeteryId;
    this.store.dispatch(new AddPerson(person)).subscribe(() => this.updateList());
  }

  changePerson(id: string) {
    const filter: PersonFilter = { id };
    this.store.dispatch(new GetPerson(filter)).subscribe(() => {
      const person = this.store.selectSnapshot<PersonDto>(PersonState.person);
      const { id, lastname, firstname, patronymic, cemeteryId, startDate, finishDate } = person;
      this.selectedPerson = person as Person;
      this.personForm.setValue({ id, lastname, firstname, patronymic, cemeteryId, startDate, finishDate });
    });
  }

  markAsRemovedPerson(id: string) {
    this.store.dispatch(new MarkAsRemovedPerson(id)).subscribe(() => this.updateList());
  }

  updatePerson() {
    const person: PersonDto = this.personForm.value;
    console.log('SEND', person);
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
      this.resetForm();
    });
  }
}
