import { State, Selector, Action, StateContext } from '@ngxs/store';
import { Injectable } from '@angular/core';
import { User, UserDto } from '@models';
import { GetUser } from '@actions';
import { HttpParams } from '@angular/common/http';
import { ApiService } from '@core';
import { tap } from 'rxjs/operators';
import { Observable } from 'rxjs';

export interface UserStateModel {
  user: User;
  userList: Array<User>;
}

@State<UserStateModel>({
  name: 'user',
  defaults: { user: null, userList: [] },
})
@Injectable()
export class UserState {
  constructor(private apiService: ApiService) {}
  
  @Selector()
  static user({ user }: UserStateModel): User {
    return user;
  }

  @Action(GetUser)
  getUser(ctx: StateContext<UserStateModel>, { payload: filter }): Observable<any> {
    const params: HttpParams = filter;
    return this.apiService.get<Array<UserDto>>('user', params).pipe(tap(userList => ctx.patchState({ user: userList[0] as User })));
  }
}
