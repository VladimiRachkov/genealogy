import { ProductForUserOutDto, UserFilter } from "@models";

export class SetAdminMode {
  static readonly type = '[Main] Set Admin Mode';
  constructor(readonly payload: boolean) {}
}

export class SetAuthorization {
  static readonly type = '[Main] Set Authorization';
  constructor(readonly payload: boolean) {}
}

export class FetchActiveSubscription {
  static readonly type = '[Main] Fetch ActiveSubscription';
  constructor(readonly payload?: UserFilter) {}
}

export class FetchPurchases {
  static readonly type = '[Main] Fetch Purchases';
}

export class FetchBook {
  static readonly type = '[Main] Fetch Book';
}

export class AddPurchaseForUser {
  static readonly type = "[Main] Add Purchase For User"
  constructor(readonly payload: ProductForUserOutDto) {}
}