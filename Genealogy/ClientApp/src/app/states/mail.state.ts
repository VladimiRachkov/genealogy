import { CreateMessage, FetchMessage, FetchMessageList, GetMessagesCount, UpdateMessage } from '@actions';
import { HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BusinessObject, BusinessObjectInDto, BusinessObjectOutDto } from '@models';
import { Action, Selector, State, StateContext } from '@ngxs/store';
import { BusinessObjectService } from '@repository';
import { tap } from 'rxjs/operators';
import { first } from 'lodash';

export interface MailStateModel {
  item: BusinessObjectInDto;
  list: BusinessObjectInDto[];
  count: number;
}

@State<MailStateModel>({
  name: 'mail',
  defaults: {
    item: null,
    list: [],
    count: 0,
  },
})
@Injectable()
export class MailState {
  constructor(private boService: BusinessObjectService) {}

  @Selector()
  static list({ list }: MailStateModel): BusinessObject[] {
    return list;
  }

  @Selector()
  static item({ item }: MailStateModel): BusinessObject {
    return item;
  }

  @Selector()
  static count({ count }: MailStateModel): number {
    return count;
  }

  @Action(FetchMessageList)
  fetchMessageList(ctx: StateContext<MailStateModel>, { payload: filter }) {
    const params: HttpParams = filter;
    return this.boService.FetchBusinessObjectList(params).pipe(tap(list => ctx.patchState({ list })));
  }

  @Action(FetchMessage)
  fetchMessage(ctx: StateContext<MailStateModel>, { payload: filter }) {
    const params: HttpParams = filter;
    return this.boService.FetchBusinessObject(params).pipe(tap(items => ctx.patchState({ item: first(items) })));
  }

  @Action(CreateMessage)
  createMessage(ctx: StateContext<MailStateModel>, { payload }) {
    const body: BusinessObjectOutDto = payload;
    return this.boService.AddBusinessObject(body).pipe(tap(item => ctx.patchState({ item })));
  }

  @Action(GetMessagesCount)
  getMessagesCount(ctx: StateContext<MailStateModel>, { payload: filter }) {
    const params: HttpParams = filter;
    return this.boService.GetBusinessObjectsCount(params).pipe(tap(({ count }) => ctx.patchState({ count })));
  }

  @Action(UpdateMessage)
  updateCatalogItem(ctx: StateContext<MailStateModel>, { payload }) {
    const body: BusinessObjectOutDto = payload;
    return this.boService.UpdateBusinessObject(body).pipe(tap(item => ctx.patchState({ item })));
  }
}
