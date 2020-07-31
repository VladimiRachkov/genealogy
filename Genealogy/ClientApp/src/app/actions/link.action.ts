import { LinkDto, LinkFilter } from '@models';

export class AddLink {
  static readonly type = '[Link] Add Link';
  constructor(readonly payload: LinkDto) {}
}

export class FetchLinkList {
  static readonly type = '[Link] Fetch Link List';
  constructor(readonly payload: LinkFilter) {}
}
