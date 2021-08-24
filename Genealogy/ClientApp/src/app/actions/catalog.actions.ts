import { BusinessObjectFilter, BusinessObjectOutDto } from '@models';

export class FetchCatalogItem {
  static readonly type = '[Catalog] Fetch Catalog Item';
  constructor(readonly payload: BusinessObjectFilter) {}
}

export class FetchCatalogList {
  static readonly type = '[Catalog] Fetch Catalog List';
  constructor(readonly payload: BusinessObjectFilter) {}
}

export class AddCatalogItem {
  static readonly type = '[Catalog] Add Catalog Item';
  constructor(readonly payload: BusinessObjectOutDto) {}
}

export class UpdateCatalogItem {
  static readonly type = '[Catalog] Update Catalog Item';
  constructor(readonly payload: BusinessObjectOutDto) {}
}

export class GetCatalogItemsCount {
  static readonly type = '[Catalog] Get Catalog Item Count';
  constructor(readonly payload: BusinessObjectFilter) {}
}

export class FetchPurchaseList {
  static readonly type = '[Catalog] Fetch Purchase List';
  constructor(readonly payload: BusinessObjectFilter) {}
}

export class ActivatePurchase {
  static readonly type = '[Catalog] Activate Purchase';
  constructor(readonly payload: BusinessObjectFilter) {}
}

export class RemovePurchase {
  static readonly type = '[Catalog] Remove Purchase';
  constructor(readonly payload: string) {}
}

