import { Injectable } from '@angular/core';
import { State, Selector, StateContext, Action } from '@ngxs/store';
import { AddPurchaseForUser, FetchActiveSubscription, FetchBook, FetchPurchases, SetAdminMode, SetAuthorization } from '@actions';
import { BusinessObject, BusinessObjectFilter, BusinessObjectInDto, UserFilter } from '@models';
import { ApiService, AuthenticationService } from '@core';
import { tap } from 'rxjs/operators';
import { isNil } from 'lodash';
import { BusinessObjectService } from '@repository';
import { HttpParams } from '@angular/common/http';
import { METATYPE_ID } from '@enums';
import { Observable } from 'rxjs';

export interface MainStateModel {
  adminMode: boolean;
  hasAuth: boolean;
  subscription: BusinessObject;
  purchases: BusinessObject[];
  book: BusinessObject;
  addSubscribleFailed: boolean;
}

@State<MainStateModel>({
  name: 'main',
  defaults: { adminMode: false, hasAuth: false, subscription: null, purchases: null, book: null, addSubscribleFailed: null },
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

  @Selector()
  static book({ book }: MainStateModel): BusinessObject {
    return book;
  }

  @Selector()
  static addSubscribleFailed({ addSubscribleFailed }: MainStateModel): boolean {
    return addSubscribleFailed;
  }


  @Action(SetAdminMode)
  setAdminMode(ctx: StateContext<MainStateModel>, { payload: adminMode }: SetAdminMode) {
    ctx.patchState({ adminMode });
  }

  @Action(SetAuthorization)
  setAuthorization(ctx: StateContext<MainStateModel>, { payload: hasAuth }: SetAuthorization) {
    ctx.patchState({ hasAuth });
  }

  @Action(FetchActiveSubscription)
  fetchActiveSubscription(ctx: StateContext<MainStateModel>, { payload: filter }): Observable<any> {
    const params: HttpParams = filter;
    return this.apiService.get<BusinessObjectInDto>('subscription', params).pipe(tap(subscription => ctx.patchState({ subscription })));
  }

  @Action(FetchPurchases)
  fetchPurchases(ctx: StateContext<MainStateModel>): FetchPurchases {
    const userId = this.authService.getUserId();
    const filter: BusinessObjectFilter = { metatypeId: METATYPE_ID.PURCHASE, index: 0, step: 1000, userId };
    const params: HttpParams = filter as any;
    return this.boService.FetchBusinessObjectList(params).pipe(tap(purchases => ctx.patchState({ purchases })));
  }

  @Action(FetchBook)
  fetchBook(ctx: StateContext<MainStateModel>): FetchBook {
    const userId = this.authService.getUserId();
    const filter: BusinessObjectFilter = { metatypeId: METATYPE_ID.BOOK, index: 0, step: 1, userId };
    const params: HttpParams = filter as any;
    return this.boService.FetchBusinessObjectList(params).pipe(tap(book => ctx.patchState({ book: book[0] })));
  }

  @Action(AddPurchaseForUser)
  addPurchaseForUser(ctx: StateContext<MainStateModel>, { payload: purchase }: AddPurchaseForUser) {
    return this.apiService.post<boolean>('purchase/new', purchase).pipe(tap(result => ctx.patchState({ addSubscribleFailed: result })));
  }
}
