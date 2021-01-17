import { PersonFilter, PersonDto } from '@models';

export class FetchPersonList {
  static readonly type = '[Person] Fetch Person List';
  constructor(readonly payload: PersonFilter) {}
}

export class ClearPersonList {
  static readonly type = '[Person] Clear Person List';
  constructor() {}
}

export class GetPerson {
  static readonly type = '[Person] Get Person';
  constructor(readonly payload: PersonFilter) {}
}

export class AddPerson {
  static readonly type = '[Person] Add Person';
  constructor(readonly payload: PersonDto) {}
}

export class MarkAsRemovedPerson {
  static readonly type = '[Person] Mark As Removed Person';
  constructor(readonly payload: string) {}
}

export class UpdatePerson {
  static readonly type = '[Person] Update Person';
  constructor(readonly payload: PersonDto) {}
}

export class GetPersonsCount {
  static readonly type = '[Person] Get Persons Count';
}
