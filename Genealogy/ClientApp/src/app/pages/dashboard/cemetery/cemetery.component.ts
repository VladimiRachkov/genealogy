import { Component, OnInit } from '@angular/core';
import { Cemetery } from '@mdl/cemetery';
import { Store } from '@ngxs/store';
import { GetCemetery, AddCemetery, MarkAsRemovedCemetery, FetchCemeteryList, UpdateCemetery } from '../../../actions/cemetery.actions';
import { CemeteryDto } from '@mdl/dtos/cemetery.dto';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { CemeteryFilter } from '@mdl/filters/cemetery.filter';
import { Table } from '@shared/components/dashboard/table/table';
import { CemeteryState } from '@state/cemetery.state';

@Component({
  selector: 'dashboard-cemetery',
  templateUrl: './cemetery.component.html',
  styleUrls: ['./cemetery.component.scss'],
  providers: [FormBuilder],
})
export class CemeteryComponent implements OnInit {
  cemeteryList: Array<Cemetery>;
  cemeteryForm: FormGroup;
  selectedCemetery: Cemetery;
  tableData: Table.Data;

  constructor(private store: Store) {}

  ngOnInit() {
    this.updateList();
    this.cemeteryForm = new FormGroup({
      id: new FormControl(null),
      name: new FormControl(null, [Validators.required]),
      location: new FormControl(null, [Validators.required]),
    });
    this.selectedCemetery = null;
  }

  addCemetery() {
    const cemetery = this.cemeteryForm.value as CemeteryDto;
    this.store.dispatch(new AddCemetery(cemetery)).subscribe(() => this.updateList());
  }

  markAsRemovedCemetery(id: string) {
    this.store.dispatch(new MarkAsRemovedCemetery(id)).subscribe(() => this.updateList());
  }

  changeCemetery(id: string) {
    const filter: CemeteryFilter = { id };
    this.store.dispatch(new GetCemetery(filter)).subscribe(() => {
      const cemetery = this.store.selectSnapshot<Cemetery>(CemeteryState.cemetery);
      const { id, name, location } = cemetery;
      this.selectedCemetery = cemetery;
      this.cemeteryForm.setValue({ id, name, location });
    });
  }

  updateCemetery() {
    const cemetery = this.cemeteryForm.value;
    this.store.dispatch(new UpdateCemetery(cemetery)).subscribe(() => this.updateList());
  }

  resetForm() {
    this.cemeteryForm.getRawValue();
    this.selectedCemetery = null;
  }

  private updateList() {
    this.store.dispatch(new FetchCemeteryList()).subscribe(() => {
      this.cemeteryList = this.store.selectSnapshot<Array<Cemetery>>(CemeteryState.cemeteryList);

      const items = this.cemeteryList.map<Table.Item>(item => ({ id: item.id, values: [item.name, item.location] }));

      this.tableData = {
        fields: ['Название', 'Расположение'],
        items,
      };
      this.resetForm();
    });
  }
}
