import { PageFilter } from '@mdl/filters/page.filter';
import { PageDto } from '@mdl/dtos/page.dto';

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