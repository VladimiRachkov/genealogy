import { AddCatalogItem, FetchCatalogItem, FetchCatalogList, GetCatalogItemsCount, UpdateCatalogItem } from '@actions';
import { HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BusinessObject, BusinessObjectInDto, BusinessObjectOutDto } from '@models';
import { Action, Selector, State, StateContext } from '@ngxs/store';
import { CatalogService } from '@repository';
import { tap } from 'rxjs/operators';
import { first } from 'lodash';

export interface CatalogStateModel {
  item: BusinessObjectInDto;
  list: BusinessObjectInDto[];
  count: number;
}

@State<CatalogStateModel>({
  name: 'catalog',
  defaults: {
    item: null,
    list: [],
    count: null,
  },
})
@Injectable()
export class CatalogState {
  constructor(private catalogService: CatalogService) {}

  @Selector()
  static list({ list }: CatalogStateModel): BusinessObject[] {
    return list;
  }

  @Selector()
  static item({ item }: CatalogStateModel): BusinessObject {
    return item;
  }

  @Selector()
  static count({ count }: CatalogStateModel): number {
    return count;
  }

  @Action(FetchCatalogList)
  fetchCatalogList(ctx: StateContext<CatalogStateModel>, { payload: filter }) {
    const params: HttpParams = filter;
    return this.catalogService.FetchCatalogList(params).pipe(tap(list => ctx.patchState({ list })));
  }

  @Action(FetchCatalogItem)
  fetchCatalogItem(ctx: StateContext<CatalogStateModel>, { payload: filter }) {
    const params: HttpParams = filter;
    return this.catalogService.FetchCatalogItem(params).pipe(tap(items => ctx.patchState({ item: first(items) })));
  }

  @Action(AddCatalogItem)
  addCatalogItem(ctx: StateContext<CatalogStateModel>, { payload }) {
    const body: BusinessObjectOutDto = payload;
    return this.catalogService.AddCatalogItem(body).pipe(tap(item => ctx.patchState({ item })));
  }

  @Action(UpdateCatalogItem)
  updateCatalogItem(ctx: StateContext<CatalogStateModel>, { payload }) {
    const body: BusinessObjectOutDto = payload;
    return this.catalogService.UpdateCatalogItem(body).pipe(tap(item => ctx.patchState({ item })));
  }

  @Action(GetCatalogItemsCount)
  getCatalotItemsCount(ctx: StateContext<CatalogStateModel>, { payload: filter }) {
    const params: HttpParams = filter;
    return this.catalogService.GetCatalogItemsCount(params).pipe(
      tap(count => console.log('COUNT', count)),
      tap(({ count }) => ctx.patchState({ count }))
    );
  }
}
