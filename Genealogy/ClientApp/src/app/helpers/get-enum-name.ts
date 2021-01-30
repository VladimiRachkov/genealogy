import { $enum } from 'ts-enum-util';
import { USER_STATUS } from '@enums';

const EnumList = { ...USER_STATUS };

export const GetEnumName = (value: string): string => {
  return $enum(EnumList).getKeyOrDefault(value);
};
