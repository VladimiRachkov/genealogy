import { Component, OnInit, OnDestroy } from '@angular/core';
import { Store, Select } from '@ngxs/store';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { Table, Person, Cemetery, PersonDto, PersonFilter, Paginator, CemeteryFilter } from '@models';
import { CemeteryState, CountyState, PersonState } from '@states';
import { AddPerson, UpdatePerson, FetchPerson, FetchCemeteryList, FetchPersonList, FetchCountyList, GetCemeteries } from '@actions';
import { FileUploadService } from 'app/core/services/file-upload.service';
import { NotifierService } from 'angular-notifier';
import { isEmpty } from 'lodash';
import { count } from 'console';

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
  searchForm: FormGroup;
  cemeteries: Array<{ id: string; name: string }>;
  counties: Array<{ id: string; name: string }>;
  filter: PersonFilter = {};

  selectedCountyId?: string

  @Select(CemeteryState.cemeteryList) cemeteryList$: Observable<Array<Cemetery>>;

  constructor(private store: Store, private fileUploadService: FileUploadService, private notifierService: NotifierService) {}

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

    this.searchForm = new FormGroup({
      fio: new FormControl(null, [Validators.required]),
      countyId: new FormControl(null, [Validators.required]),
      cemeteryId: new FormControl(null, [Validators.required]),
    });

    this.store.dispatch(new FetchCountyList()).subscribe(() => {
      const countyList = this.store.selectSnapshot(CountyState.countyList);
      this.counties = countyList.map(({ id, name }) => ({ id, name }));
    });

    this.searchForm.valueChanges.subscribe(value => {
      if (isEmpty(value.fio) && isEmpty(value.cemeteryId)) {
        this.filter = {};
      } else {
        const { fio, cemeteryId } = value;
        this.filter = { fio, cemeteryId };
      }
      this.updateList();
    });
    this.updateList();
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
    this.store.dispatch(new UpdatePerson({ id, isRemoved: true })).subscribe(() => this.updateList());
  }

  onRestore(id: string) {
    this.store.dispatch(new UpdatePerson({ id, isRemoved: false })).subscribe(() => this.updateList());
  }

  onUpdate() {
    const person: PersonDto = this.personForm.value;
    this.store.dispatch(new UpdatePerson(person)).subscribe(() => this.updateList());
  }

  private updateList() {
    this.store.dispatch(new FetchPersonList(this.filter)).subscribe(() => {
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

  onUploadDocFile(file) {
    this.fileUploadService.postFile(file[0]).subscribe(({ result, message }) => this.notifierService.notify(result, message));
  }

  onChangeCounty() {
    this.selectedCountyId = this.searchForm.value['countyId'];
    this.getCemeteries()
  }

  private getCemeteries() {
    const filter: CemeteryFilter = { countyId: this.selectedCountyId }
    this.store.dispatch(new GetCemeteries(filter)).subscribe(() => {
      const cemeteryList = this.store.selectSnapshot(CemeteryState.cemeteryList);
      this.cemeteries = cemeteryList.map(({ id, name }) => ({ id, name }));
    });
  }
}
