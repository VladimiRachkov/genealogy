import { PageFilter, PageDto } from '@models';

export class FetchPageList {
  static readonly type = '[Page] Fetch Page List';
  constructor(readonly payload: PageFilter) {}
}

export class GetPage {
  static readonly type = '[Page] Get Page';
  constructor(readonly payload: PageFilter) {}
}

export class AddPage {
  static readonly type = '[Page] Add Page';
  constructor(readonly payload: PageDto) {}
}

export class MarkAsRemovedPage {
  static readonly type = '[Page] Mark As Removed Page';
  constructor(readonly payload: string) {}
}

export class UpdatePage {
  static readonly type = '[Page] Update Page';
  constructor(readonly payload: PageDto) {}
}

export class FetchFreePageList {
  static readonly type = '[Page] Free Page List';
  constructor(readonly payload: string) {}
}

export class GetPageWithLinks {
  static readonly type = '[Page] Get Page With Links';
  constructor(readonly payload: PageFilter) {}
}
