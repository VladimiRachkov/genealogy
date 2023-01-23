import { Component, OnInit } from '@angular/core';
import { Store } from '@ngxs/store';
import {
  GetCounty,
  AddCounty,
  FetchCountyList,
  UpdateCounty,
  RemoveCounty,
  RestoreCounty,
} from '../../../actions/county.actions';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { County, Table, CountyDto, CountyFilter } from '@models';
import { CountyState } from '@states';

@Component({
  selector: 'dashboard-county',
  templateUrl: './county.component.html',
  styleUrls: ['./county.component.scss'],
  providers: [FormBuilder],
})
export class CountyComponent implements OnInit {
  countyList: Array<County>;
  countyForm: FormGroup;
  county: County;
  tableData: Table.Data;

  constructor(private store: Store) {}

  ngOnInit() {
    this.updateList();
    this.countyForm = new FormGroup({
      id: new FormControl(null),
      name: new FormControl(null, [Validators.required]),
      coords: new FormControl(null)
    });
    this.county = null;
  }

  onAdd() {
    const county = this.countyForm.value as CountyDto;
    this.store.dispatch(new AddCounty(county)).subscribe(() => this.updateList());
  }

  onRemove(id: string) {
    this.store.dispatch(new RemoveCounty(id)).subscribe(() => this.updateList());
  }

  onRestore(id: string) {
    this.store.dispatch(new RestoreCounty(id)).subscribe(() => this.updateList());
  }

  onSelect(id: string) {
    const filter: CountyFilter = { id };
    this.store.dispatch(new GetCounty(filter)).subscribe(() => {
      const county = this.store.selectSnapshot<County>(CountyState.county);
      const { id, name, coords } = county;
      this.county = county;
      this.countyForm.setValue({ id, name, coords });
    });
  }

  onUpdate() {
    const county = this.countyForm.value;
    this.store.dispatch(new UpdateCounty(county)).subscribe(() => this.updateList());
  }

  onReset() {
    this.reset();
  }

  private updateList() {
    this.store.dispatch(new FetchCountyList()).subscribe(() => {
      this.countyList = this.store.selectSnapshot<Array<County>>(CountyState.countyList);

      const items = this.countyList.map<Table.Item>(item => ({
        id: item.id,
        values: [item.name, item.coords],
        isRemoved: item.isRemoved,
      }));

      this.tableData = {
        fields: ['Название', 'Координаты'],
        items,
      };
      this.reset();
    });
  }

  private reset() {
    this.countyForm.getRawValue();
    this.county = null;
  }
}
