import { CountyFilter, CountyDto } from '@models';

export class FetchCountyList {
  static readonly type = '[County] Fetch County List';
}

export class GetCounty {
  static readonly type = '[County] Get County';
  constructor(readonly payload: CountyFilter) {}
}

export class AddCounty {
  static readonly type = '[County] Add County';
  constructor(readonly payload: CountyDto) {}
}

export class RemoveCounty {
  static readonly type = '[County] Remove County';
  constructor(readonly payload: string) {}
}

export class RestoreCounty {
  static readonly type = '[County] Restore County';
  constructor(readonly payload: string) {}
}


export class UpdateCounty {
  static readonly type = '[County] Update County';
  constructor(readonly payload: CountyDto) {}
}