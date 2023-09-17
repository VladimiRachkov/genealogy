import { UserDto, UserFilter } from '@models';

export class FetchUserList {
  static readonly type = '[User] Fetch User List';
  constructor(readonly payload: UserFilter) {}
}

export class GetUser {
  static readonly type = '[User] Get User';
  constructor(readonly payload: UserFilter) {}
}

export class UpdateUser {
  static readonly type = '[User] Update User';
  constructor(readonly payload: UserDto) {}
}

export class CreatePassword {
  static readonly type = '[User] Create Password';
  constructor(readonly payload: UserDto) {}
}
