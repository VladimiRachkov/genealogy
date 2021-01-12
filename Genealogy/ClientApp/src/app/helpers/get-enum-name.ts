import { $enum } from 'ts-enum-util';
import { USER_STATUSES } from '@enums';

const EnumList = { ...USER_STATUSES };

export const GetEnumName = (value: string): string => {
  return $enum(EnumList).getKeyOrDefault(value);
};
