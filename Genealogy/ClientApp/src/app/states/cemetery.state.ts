import { Injectable } from '@angular/core';
import { GetCemetery, AddCemetery, FetchCemeteryList, UpdateCemetery, RemoveCemetery, RestoreCemetery, GetCemeteries } from '../actions/cemetery.actions';
import { State, Action, StateContext, Selector } from '@ngxs/store';
import { tap } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { HttpParams } from '@angular/common/http';
import { CemeteryDto, Cemetery } from '@models';
import { ApiService } from '@core';

export interface CemeteryStateModel {
  cemeteryList: Array<CemeteryDto>;
  cemetery: CemeteryDto;
}

@State<CemeteryStateModel>({
  name: 'cemetery',
  defaults: { cemeteryList: [], cemetery: null },
})
@Injectable()
export class CemeteryState {
  constructor(private apiService: ApiService) {}

  @Selector()
  static cemetery({ cemetery }: CemeteryStateModel): Cemetery {
    return cemetery as Cemetery;
  }

  @Selector()
  static cemeteryList({ cemeteryList }: CemeteryStateModel): Array<Cemetery> {
    return cemeteryList as Array<Cemetery>;
  }

  @Action(FetchCemeteryList)
  fetchCemeteryList(ctx: StateContext<CemeteryStateModel>): Observable<Array<CemeteryDto>> {
    return this.apiService.get<Array<CemeteryDto>>('cemetery', null).pipe(tap(cemeteryList => ctx.patchState({ cemeteryList })));
  }

  @Action(GetCemetery)
  getCemetery(ctx: StateContext<CemeteryStateModel>, { payload: filter }): Observable<Array<CemeteryDto>> {
    const params: HttpParams = filter;
    return this.apiService
      .get<Array<CemeteryDto>>('cemetery', params)
      .pipe(tap(cemeteryList => ctx.patchState({ cemetery: cemeteryList[0] })));
  }

  @Action(GetCemeteries)
  getCemeteries(ctx: StateContext<CemeteryStateModel>, { payload: filter }): Observable<Array<CemeteryDto>> {
    const params: HttpParams = filter;
    return this.apiService
      .get<Array<CemeteryDto>>('cemetery', params)
      .pipe(tap(cemeteryList => ctx.patchState({ cemeteryList: cemeteryList })));
  }

  @Action(AddCemetery)
  addCemetery(ctx: StateContext<CemeteryStateModel>, { payload: cemetery }: AddCemetery): Observable<any> {
    return this.apiService.post<CemeteryDto>('cemetery', cemetery);
  }

  @Action(RemoveCemetery)
  removeCemetery(ctx: StateContext<CemeteryStateModel>, { payload: id }: RemoveCemetery): Observable<any> {
    return this.apiService.delete<CemeteryDto>('cemetery', id);
  }

  @Action(RestoreCemetery)
  restoreCemetery(ctx: StateContext<CemeteryStateModel>, { payload: id }: RestoreCemetery): Observable<any> {
    return this.apiService.restore<CemeteryDto>('cemetery', id);
  }

  @Action(UpdateCemetery)
  updateCemetery(ctx: StateContext<CemeteryStateModel>, { payload: cemetery }: UpdateCemetery): Observable<any> {
    return this.apiService.put<CemeteryDto>('cemetery', cemetery);
  }
}
