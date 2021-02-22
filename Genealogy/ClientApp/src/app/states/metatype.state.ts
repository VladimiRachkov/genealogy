import { Injectable } from '@angular/core';
import { State, Selector, StateContext, Action } from '@ngxs/store';
import { FetchMetatypeList } from '@actions';
import { Metatype, MetatypeInDto } from '@models';
import { ApiService } from '@core';
import { tap } from 'rxjs/operators';

export interface MetatypeStateModel {
  list: Metatype[];
}

@State<MetatypeStateModel>({
  name: 'metatype',
  defaults: { list: [] },
})
@Injectable()
export class MetatypeState {
  constructor(private apiService: ApiService) {}

  @Selector()
  static list({ list }: MetatypeStateModel): Metatype[] {
    return list;
  }

  @Action(FetchMetatypeList)
  fetchMetatypeList(ctx: StateContext<MetatypeStateModel>) {
    return this.apiService.get<Array<MetatypeInDto>>('metatype', null).pipe(tap(list => ctx.patchState({ list })));
  }
}
