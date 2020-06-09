import { Injectable } from '@angular/core';
import { GetCemetery, AddCemetery, MarkAsRemovedCemetery, FetchCemeteryList, UpdateCemetery } from '../actions/cemetery.actions';
import { Cemetery } from '@mdl/cemetery';
import { State, Action, StateContext, Selector } from '@ngxs/store';
import { ApiService } from '@srv/api.service';
import { tap } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { CemeteryDto } from '@mdl/dtos/cemetery.dto';
import { HttpParams } from '@angular/common/http';

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
    return cemeteryList.filter(item => !item.removed) as Array<Cemetery>;
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

  @Action(AddCemetery)
  addCemetery(ctx: StateContext<CemeteryStateModel>, { payload: cemetery }: AddCemetery): Observable<any> {
    return this.apiService.post<CemeteryDto>('cemetery', cemetery).pipe(tap(data => console.log('ADD', data)));
  }

  @Action(MarkAsRemovedCemetery)
  markAsRemovedCemetery(ctx: StateContext<CemeteryStateModel>, { payload: id }: MarkAsRemovedCemetery): Observable<any> {
    const cemeteryDto: CemeteryDto = { id, name: null, location: null };
    return this.apiService.post<CemeteryDto>('cemetery/markasremoved', cemeteryDto);
  }

  @Action(UpdateCemetery)
  updateCemetery(ctx: StateContext<CemeteryStateModel>, { payload: cemetery }: UpdateCemetery): Observable<any> {
    return this.apiService.put<CemeteryDto>('cemetery', cemetery);
  }
}
