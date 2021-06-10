import { Injectable } from '@angular/core';
import { State, Selector, StateContext, Action } from '@ngxs/store';
import { FetchActiveSubscribe, FetchPurchases, SetAdminMode, SetAuthorization } from '@actions';
import { BusinessObject, BusinessObjectFilter, BusinessObjectInDto } from '@models';
import { ApiService, AuthenticationService } from '@core';
import { tap } from 'rxjs/operators';
import { isNil } from 'lodash';
import { BusinessObjectService } from '@repository';
import { HttpParams } from '@angular/common/http';
import { METATYPE_ID } from '@enums';

export interface MainStateModel {
  adminMode: boolean;
  hasAuth: boolean;
  subscription: BusinessObject;
  purchases: BusinessObject[];
}

@State<MainStateModel>({
  name: 'main',
  defaults: { adminMode: false, hasAuth: false, subscription: null, purchases: null },
})
@Injectable()
export class MainState {
  constructor(private apiService: ApiService, private boService: BusinessObjectService, private authService: AuthenticationService) {}

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

  @Selector()
  static purchases({ purchases }: MainStateModel): BusinessObject[] {
    return purchases;
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

  @Action(FetchPurchases)
  fetchPurchases(ctx: StateContext<MainStateModel>): FetchPurchases {
    const userId = this.authService.getUserId();
    const filter: BusinessObjectFilter = { metatypeId: METATYPE_ID.PURCHASE, index: 0, step: 1000, userId };
    const params: HttpParams = filter as any;
    return this.boService.FetchBusinessObjectList(params).pipe(tap(purchases => ctx.patchState({ purchases })));
  }
}
