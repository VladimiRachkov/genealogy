import { Component, OnInit, OnDestroy } from '@angular/core';
import { Store, Select } from '@ngxs/store';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { Table, Person, Cemetery, PersonDto, PersonFilter, Paginator } from '@models';
import { CemeteryState, PersonState } from '@states';
import { AddPerson, UpdatePerson, GetPersonsCount, FetchPerson, FetchAllPersons } from '@actions';
import { FileUploadService } from 'app/core/services/file-upload.service';

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

  fileForm: FormGroup;

  paginatorOptions: Paginator;
  pageIndex: number = 0;
  step: number = 10;
  startIndex: number = 0;

  @Select(CemeteryState.cemeteryList) cemeteryList$: Observable<Array<Cemetery>>;

  constructor(private store: Store, private fileUploadService: FileUploadService) {}

  ngOnInit() {
    this.personForm = new FormGroup({
      id: new FormControl(null),
      lastname: new FormControl(null, null),
      firstname: new FormControl(null, null),
      patronymic: new FormControl(null, null),
      startDate: new FormControl(null, null),
      finishDate: new FormControl(null, null),
      cemeteryId: new FormControl(null, null),
      source: new FormControl(null, null),
      comment: new FormControl(null, null),
    });
    this.person = null;

    this.fileForm = new FormGroup({
      docFile: new FormControl(null, Validators.required),
    });

    this.updatePaginator();
  }

  ngOnDestroy() {}

  onReset() {
    this.personForm.getRawValue();
  }

  onAdd() {
    const person = this.personForm.value as PersonDto;

    this.personForm.value.cemeteryId;
    this.store.dispatch(new AddPerson(person)).subscribe(() => this.updatePaginator());
  }

  onSelect(id: string) {
    const filter: PersonFilter = { id };

    this.store.dispatch(new FetchPerson(filter)).subscribe(() => {
      this.personForm.reset();

      const person = this.store.selectSnapshot<PersonDto>(PersonState.person);
      const { id, lastname, firstname, patronymic, cemeteryId, startDate, finishDate, source, comment } = person;

      this.person = person as Person;
      this.personForm.setValue({ id, lastname, firstname, patronymic, cemeteryId, startDate, finishDate, source, comment });
    });
  }

  onRemove(id: string) {
    this.store.dispatch(new UpdatePerson({ id, isRemoved: true })).subscribe(() => this.updatePaginator());
  }

  onRestore(id: string) {
    this.store.dispatch(new UpdatePerson({ id, isRemoved: false })).subscribe(() => this.updatePaginator());
  }

  onUpdate() {
    const person: PersonDto = this.personForm.value;
    this.store.dispatch(new UpdatePerson(person)).subscribe(() => this.updateList());
  }

  private updateList(pageIndex: number = this.pageIndex) {
    this.paginatorOptions.index = pageIndex;
    const { step } = this.paginatorOptions;

    this.store.dispatch(new FetchAllPersons({ index: pageIndex, step })).subscribe(() => {
      this.personList = this.store.selectSnapshot<Array<Person>>(PersonState.personList) || [];

      const items = this.personList.map<Table.Item>(item => ({
        id: item.id,
        values: [
          `${item.lastname} ${item.firstname} ${item.patronymic}`,
          item.startDate,
          item.finishDate,
          item.cemetery ? item.cemetery.name : null,
        ],
        isRemoved: item.isRemoved,
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
    this.pageIndex = pageIndex;
    this.startIndex = pageIndex * this.step;
    this.updateList(pageIndex);
  }

  onUploadDocFile(file) {
    console.log(file);
    this.fileUploadService.postFile(file[0]).subscribe(res => {
      console.log('UPLOAD', res);
    });
  }

  private updatePaginator() {
    this.store.dispatch(new GetPersonsCount()).subscribe(data => {
      this.paginatorOptions = {
        index: 0,
        step: this.step,
        count: data.person.count,
      };

      this.updateList();
    });
  }
}
