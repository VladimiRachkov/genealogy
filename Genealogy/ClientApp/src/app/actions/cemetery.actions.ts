import { CemeteryFilter, CemeteryDto } from '@models';

export class FetchCemeteryList {
  static readonly type = '[Cemetery] Fetch Cemetery List';
}

export class GetCemetery {
  static readonly type = '[Cemetery] Get Cemetery';
  constructor(readonly payload: CemeteryFilter) {}
}

export class GetCemeteries {
  static readonly type = '[Cemetery] Get Cemeteries';
  constructor(readonly payload: CemeteryFilter) {}
}

export class AddCemetery {
  static readonly type = '[Cemetery] Add Cemetery';
  constructor(readonly payload: CemeteryDto) {}
}

export class RemoveCemetery {
  static readonly type = '[Cemetery] Remove Cemetery';
  constructor(readonly payload: string) {}
}

export class RestoreCemetery {
  static readonly type = '[Cemetery] Restore Cemetery';
  constructor(readonly payload: string) {}
}


export class UpdateCemetery {
  static readonly type = '[Cemetery] Update Cemetery';
  constructor(readonly payload: CemeteryDto) {}
}