import { Injectable } from '@angular/core';
import { GetCounty, AddCounty, FetchCountyList, UpdateCounty, RemoveCounty, RestoreCounty } from '../actions/county.actions';
import { State, Action, StateContext, Selector } from '@ngxs/store';
import { tap } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { HttpParams } from '@angular/common/http';
import { CountyDto, County } from '@models';
import { ApiService } from '@core';

export interface CountyStateModel {
  countyList: Array<CountyDto>;
  county: CountyDto;
}

@State<CountyStateModel>({
  name: 'county',
  defaults: { countyList: [], county: null },
})
@Injectable()
export class CountyState {
  constructor(private apiService: ApiService) {}

  @Selector()
  static county({ county }: CountyStateModel): County {
    return county as County;
  }

  @Selector()
  static countyList({ countyList }: CountyStateModel): Array<County> {
    return countyList as Array<County>;
  }

  @Action(FetchCountyList)
  fetchCountyList(ctx: StateContext<CountyStateModel>): Observable<Array<CountyDto>> {
    return this.apiService.get<Array<CountyDto>>('county', null).pipe(tap(countyList => ctx.patchState({ countyList })));
  }

  @Action(GetCounty)
  getCounty(ctx: StateContext<CountyStateModel>, { payload: filter }): Observable<Array<CountyDto>> {
    const params: HttpParams = filter;
    return this.apiService
      .get<Array<CountyDto>>('county', params)
      .pipe(tap(countyList => ctx.patchState({ county: countyList[0] })));
  }

  @Action(AddCounty)
  addCounty(ctx: StateContext<CountyStateModel>, { payload: county }: AddCounty): Observable<any> {
    return this.apiService.post<CountyDto>('county', county);
  }

  @Action(RemoveCounty)
  removeCounty(ctx: StateContext<CountyStateModel>, { payload: id }: RemoveCounty): Observable<any> {
    return this.apiService.delete<CountyDto>('county', id);
  }

  @Action(RestoreCounty)
  restoreCounty(ctx: StateContext<CountyStateModel>, { payload: id }: RestoreCounty): Observable<any> {
    return this.apiService.restore<CountyDto>('county', id);
  }

  @Action(UpdateCounty)
  updateCounty(ctx: StateContext<CountyStateModel>, { payload: county }: UpdateCounty): Observable<any> {
    return this.apiService.put<CountyDto>('county', county);
  }
}
