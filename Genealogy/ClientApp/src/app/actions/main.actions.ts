export class SetAdminMode {
  static readonly type = '[Main] Set Admin Mode';
  constructor(readonly payload: boolean) {}
}

export class SetAuthorization {
  static readonly type = '[Main] Set Authorization';
  constructor(readonly payload: boolean) {}
}
