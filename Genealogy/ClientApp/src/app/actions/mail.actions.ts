import { BusinessObject, BusinessObjectFilter, BusinessObjectOutDto } from '@models';

export class FetchMessageList {
  static readonly type = '[Mail] Fetch Message List';
  constructor(readonly payload: BusinessObjectFilter) {}
}

export class FetchMessage {
  static readonly type = '[Mail] Fetch Message';
  constructor(readonly payload: BusinessObjectFilter) {}
}

export class CreateMessage {
  static readonly type = '[Mail] Create Message';
  constructor(readonly payload: BusinessObjectOutDto) {}
}

export class GetMessagesCount {
  static readonly type = '[Mail] Get Messages Count';
  constructor(readonly payload: BusinessObjectFilter) {}
}

export class UpdateMessage {
    static readonly type = '[Mail] Update Message';
    constructor(readonly payload: BusinessObjectOutDto) {}
  }
  
