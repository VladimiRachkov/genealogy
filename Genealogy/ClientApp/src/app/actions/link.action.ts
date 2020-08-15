import { LinkDto, LinkFilter } from '@models';

export class AddLink {
  static readonly type = '[Link] Add Link';
  constructor(readonly payload: LinkDto) {}
}

export class FetchLinkList {
  static readonly type = '[Link] Fetch Link List';
  constructor(readonly payload: LinkFilter) {}
}

export class UpdateLinkList {
  static readonly type = '[Link] Update Link List';
  constructor(readonly payload: Array<LinkDto>) {}
}
