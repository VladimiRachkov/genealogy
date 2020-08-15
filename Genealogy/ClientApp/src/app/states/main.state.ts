import { Injectable } from '@angular/core';
import { State, Selector, StateContext, Action } from '@ngxs/store';
import { SetAdminMode, SetAuthorization } from '@actions';


export interface MainStateModel {
  adminMode: boolean;
  hasAuth: boolean;
}

@State<MainStateModel>({
  name: 'main',
  defaults: { adminMode: false, hasAuth: false },
})
@Injectable()
export class MainState {
  @Selector()
  static adminMode({ adminMode }: MainStateModel): boolean {
    return adminMode;
  }

  @Selector()
  static hasAuth({ hasAuth }: MainStateModel): boolean {
    return hasAuth;
  }

  @Action(SetAdminMode)
  setAdminMode(ctx: StateContext<MainStateModel>, { payload: adminMode }: SetAdminMode) {
    ctx.patchState({ adminMode });
  }

  @Action(SetAuthorization)
  setAuthorization(ctx: StateContext<MainStateModel>, { payload: hasAuth }: SetAuthorization) {
    ctx.patchState({ hasAuth });
  }
}
