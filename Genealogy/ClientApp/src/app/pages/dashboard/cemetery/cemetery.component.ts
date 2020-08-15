import { Component, OnInit } from '@angular/core';
import { Store } from '@ngxs/store';
import { GetCemetery, AddCemetery, MarkAsRemovedCemetery, FetchCemeteryList, UpdateCemetery } from '../../../actions/cemetery.actions';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { Cemetery, Table, CemeteryDto, CemeteryFilter } from '@models';
import { CemeteryState } from '@states';

@Component({
  selector: 'dashboard-cemetery',
  templateUrl: './cemetery.component.html',
  styleUrls: ['./cemetery.component.scss'],
  providers: [FormBuilder],
})
export class CemeteryComponent implements OnInit {
  cemeteryList: Array<Cemetery>;
  cemeteryForm: FormGroup;
  cemetery: Cemetery;
  tableData: Table.Data;

  constructor(private store: Store) {}

  ngOnInit() {
    this.updateList();
    this.cemeteryForm = new FormGroup({
      id: new FormControl(null),
      name: new FormControl(null, [Validators.required]),
      location: new FormControl(null, [Validators.required]),
    });
    this.cemetery = null;
  }

  onAdd() {
    const cemetery = this.cemeteryForm.value as CemeteryDto;
    this.store.dispatch(new AddCemetery(cemetery)).subscribe(() => this.updateList());
  }

  onRemove(id: string) {
    this.store.dispatch(new MarkAsRemovedCemetery(id)).subscribe(() => this.updateList());
  }

  onSelect(id: string) {
    const filter: CemeteryFilter = { id };
    this.store.dispatch(new GetCemetery(filter)).subscribe(() => {
      const cemetery = this.store.selectSnapshot<Cemetery>(CemeteryState.cemetery);
      const { id, name, location } = cemetery;
      this.cemetery = cemetery;
      this.cemeteryForm.setValue({ id, name, location });
    });
  }

  onUpdate() {
    const cemetery = this.cemeteryForm.value;
    this.store.dispatch(new UpdateCemetery(cemetery)).subscribe(() => this.updateList());
  }

  onReset() {
    this.reset();
  }

  private updateList() {
    this.store.dispatch(new FetchCemeteryList()).subscribe(() => {
      this.cemeteryList = this.store.selectSnapshot<Array<Cemetery>>(CemeteryState.cemeteryList);

      const items = this.cemeteryList.map<Table.Item>(item => ({ id: item.id, values: [item.name, item.location] }));

      this.tableData = {
        fields: ['Название', 'Расположение'],
        items,
      };
      this.reset();
    });
  }

  private reset() {
    this.cemeteryForm.getRawValue();
    this.cemetery = null;
  }
}
