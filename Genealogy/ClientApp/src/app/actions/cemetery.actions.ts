import { CemeteryFilter, CemeteryDto } from '@models';

export class FetchCemeteryList {
  static readonly type = '[Cemetery] Fetch Cemetery List';
}

export class GetCemetery {
  static readonly type = '[Cemetery] Get Cemetery';
  constructor(readonly payload: CemeteryFilter) {}
}

export class AddCemetery {
  static readonly type = '[Cemetery] Add Cemetery';
  constructor(readonly payload: CemeteryDto) {}
}

export class MarkAsRemovedCemetery {
  static readonly type = '[Cemetery] Mark As Removed Cemetery';
  constructor(readonly payload: string) {}
}

export class UpdateCemetery {
  static readonly type = '[Cemetery] Update Cemetery';
  constructor(readonly payload: CemeteryDto) {}
}
