import { PersonFilter, PersonDto } from '@models';

export class FetchPersonList {
  static readonly type = '[Person] Fetch Person List';
  constructor(readonly payload: PersonFilter) {}
}

export class ClearPersonList {
  static readonly type = '[Person] Clear Person List';
  constructor() {}
}

export class FetchPerson {
  static readonly type = '[Person] Get Person';
  constructor(readonly payload: PersonFilter) {}
}

export class FetchAllPersons {
  static readonly type = '[Person] Get All Persons';
  constructor(readonly payload: PersonFilter) {}
}

export class AddPerson {
  static readonly type = '[Person] Add Person';
  constructor(readonly payload: PersonDto) {}
}

export class UpdatePerson {
  static readonly type = '[Person] Update Person';
  constructor(readonly payload: PersonDto) {}
}

export class GetPersonsCount {
  static readonly type = '[Person] Get Persons Count';
}
