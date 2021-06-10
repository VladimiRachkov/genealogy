import {
  CreateSetting,
  FetchSetting,
  FetchSettingList,
  GetSettingsCount,
  UpdateSetting,
} from '@actions';
import { HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BusinessObject, BusinessObjectInDto, BusinessObjectOutDto } from '@models';
import { Action, Selector, State, StateContext } from '@ngxs/store';
import { BusinessObjectService } from '@repository';
import { tap } from 'rxjs/operators';
import { first } from 'lodash';
import { METATYPE_ID } from '@enums';

const metatypeId = METATYPE_ID.SETTING;

export interface SettingStateModel {
  item: BusinessObjectInDto;
  list: BusinessObjectInDto[];
  count: number;
}

@State<SettingStateModel>({
  name: 'settings',
  defaults: {
    item: null,
    list: [],
    count: 0,
  },
})
@Injectable()
export class SettingState {
  constructor(private boService: BusinessObjectService) {}

  @Selector()
  static list({ list }: SettingStateModel): BusinessObject[] {
    return list;
  }

  @Selector()
  static item({ item }: SettingStateModel): BusinessObject {
    return item;
  }

  @Selector()
  static count({ count }: SettingStateModel): number {
    return count;
  }

  @Action(FetchSettingList)
  fetchMessageList(ctx: StateContext<SettingStateModel>, { payload: filter }) {
    const params: HttpParams = { ...filter, metatypeId };
    return this.boService.FetchBusinessObjectList(params).pipe(tap(list => ctx.patchState({ list })));
  }

  @Action(FetchSetting)
  fetchMessage(ctx: StateContext<SettingStateModel>, { payload: filter }) {
    const params: HttpParams = { ...filter, metatypeId };
    return this.boService.FetchBusinessObject(params).pipe(tap(items => ctx.patchState({ item: first(items) })));
  }

  @Action(CreateSetting)
  createMessage(ctx: StateContext<SettingStateModel>, { payload }) {
    const body: BusinessObjectOutDto = { ...payload, metatypeId };
    return this.boService.AddBusinessObject(body).pipe(tap(item => ctx.patchState({ item })));
  }

  @Action(GetSettingsCount)
  getMessagesCount(ctx: StateContext<SettingStateModel>) {
    const filter: any = { metatypeId };
    const params: HttpParams = filter;
    return this.boService.GetBusinessObjectsCount(params).pipe(tap(({ count }) => ctx.patchState({ count })));
  }

  @Action(UpdateSetting)
  updateCatalogItem(ctx: StateContext<SettingStateModel>, { payload }) {
    const body: BusinessObjectOutDto = { ...payload, metatypeId };
    return this.boService.UpdateBusinessObject(body).pipe(tap(item => ctx.patchState({ item })));
  }
}
