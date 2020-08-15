import { UserFilter } from '@models';

export class FetchUserList {
  static readonly type = '[Person] Fetch User List';
  constructor(readonly payload: UserFilter) {}
}

export class GetUser {
  static readonly type = '[Person] Get User';
  constructor(readonly payload: UserFilter) {}
}
