import { Injectable } from '@angular/core';
import { State, Selector, StateContext, Action } from '@ngxs/store';
import { FetchActiveSubscribe, SetAdminMode, SetAuthorization } from '@actions';
import { BusinessObject, BusinessObjectInDto } from '@models';
import { ApiService } from '@core';
import { tap } from 'rxjs/operators';
import { isNil } from 'lodash';

export interface MainStateModel {
  adminMode: boolean;
  hasAuth: boolean;
  subscription: BusinessObject;
}

@State<MainStateModel>({
  name: 'main',
  defaults: { adminMode: false, hasAuth: false, subscription: null },
})
@Injectable()
export class MainState {
  constructor(private apiService: ApiService) {}

  @Selector()
  static adminMode({ adminMode }: MainStateModel): boolean {
    return adminMode;
  }

  @Selector()
  static hasAuth({ hasAuth }: MainStateModel): boolean {
    return hasAuth;
  }

  @Selector()
  static hasSubscription({ subscription }: MainStateModel): boolean {
    return !isNil(subscription);
  }

  @Selector()
  static subscription({ subscription }: MainStateModel): BusinessObject {
    return subscription;
  }

  @Action(SetAdminMode)
  setAdminMode(ctx: StateContext<MainStateModel>, { payload: adminMode }: SetAdminMode) {
    ctx.patchState({ adminMode });
  }

  @Action(SetAuthorization)
  setAuthorization(ctx: StateContext<MainStateModel>, { payload: hasAuth }: SetAuthorization) {
    ctx.patchState({ hasAuth });
  }

  @Action(FetchActiveSubscribe)
  fetchActiveSubscribe(ctx: StateContext<MainStateModel>): FetchActiveSubscribe {
    return this.apiService.get<BusinessObjectInDto>('subscription', null).pipe(tap(subscription => ctx.patchState({ subscription })));
  }
}
