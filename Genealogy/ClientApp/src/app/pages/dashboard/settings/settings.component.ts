import { CreateSetting, FetchSetting, FetchSettingList, GetSettingsCount, UpdateSetting } from '@actions';
import { DatePipe } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { METATYPE_ID } from '@enums';
import { BusinessObject, BusinessObjectFilter, BusinessObjectOutDto, Paginator, Table } from '@models';
import { Select, Store } from '@ngxs/store';
import { ModalComponent } from '@shared';
import { SettingState } from '@states';
import { MailState } from 'app/states/mail.state';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

const metatypeId = METATYPE_ID.SETTING;

@Component({
  selector: 'dashboard-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.scss'],
  providers: [DatePipe],
})
export class SettingsComponent implements OnInit {
  @ViewChild(ModalComponent, { static: false }) purchaseModal: ModalComponent;

  @Select(SettingState.item)
  item$: Observable<BusinessObject>;

  @Select(SettingState.list)
  list$: Observable<BusinessObject[]>;

  tableData$: Observable<Table.Data>;

  settingsForm: FormGroup;

  selectedId: string;
  selectedSetting: BusinessObject;

  constructor(private store: Store) {}

  ngOnInit() {
    this.settingsForm = new FormGroup({
      id: new FormControl(null),
      name: new FormControl(null, [Validators.required]),
      data: new FormControl(null, [Validators.required]),
    });

    this.tableData$ = this.list$.pipe(
      map<BusinessObject[], Table.Item[]>(list => list.map(({ id, name, data }) => ({ id, values: [name, data] }))),
      map<Table.Item[], Table.Data>(items => ({
        fields: ['Параметр', 'Значение'],
        items,
      }))
    );

    this.fetchList();
  }

  onAdd() {
    const { name, data } = this.settingsForm.value;
    const body = { name, data, metatypeId };

    this.store.dispatch(new CreateSetting(body)).subscribe(() => this.fetchList());
  }

  onSelect(id: string) {
    const params: BusinessObjectFilter = { id };
    this.store.dispatch(new FetchSetting(params)).subscribe(() => {
      const item = this.store.selectSnapshot<BusinessObject>(SettingState.item);
      const { id, name, data } = item;
      this.settingsForm.setValue({ id, name, data });
    });
  }

  onUpdate() {
    const { name, data } = this.settingsForm.value;
    const { id } = this.store.selectSnapshot<BusinessObject>(SettingState.item);
    this.updateItem({ id, name, data });
  }

  onRemove(id: string) {
    this.updateItem({ id, isRemoved: true });
  }

  onRestore(id: string) {
    this.updateItem({ id, isRemoved: false });
  }

  private updateItem(body: BusinessObjectOutDto) {
    this.store.dispatch(new UpdateSetting(body)).subscribe(() => this.fetchList());
    
  }

  private fetchList() {
    this.store.dispatch(new FetchSettingList()).subscribe();
  }
}
