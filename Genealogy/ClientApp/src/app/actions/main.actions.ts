export class SetAdminMode {
  static readonly type = '[Main] Set Admin Mode';
  constructor(readonly payload: boolean) {}
}

export class SetAuthorization {
  static readonly type = '[Main] Set Authorization';
  constructor(readonly payload: boolean) {}
}

export class FetchActiveSubscribe {
  static readonly type = '[Main] Fetch ActiveSubscribe';
}

export class FetchPurchases {
  static readonly type = '[Main] Fetch Purchases';
}
