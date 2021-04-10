import { BusinessObjectFilter, BusinessObjectOutDto } from '@models';

export class FetchSettingList {
  static readonly type = '[Settings] Fetch Setting List';
}

export class FetchSetting {
  static readonly type = '[Settings] Fetch Setting';
  constructor(readonly payload: BusinessObjectFilter) {}
}

export class CreateSetting {
  static readonly type = '[Settings] Create Setting';
  constructor(readonly payload: BusinessObjectOutDto) {}
}

export class GetSettingsCount {
  static readonly type = '[Settings] Get Settings Count';
}

export class UpdateSetting {
  static readonly type = '[Settings] Update Setting';
  constructor(readonly payload: BusinessObjectOutDto) {}
}
