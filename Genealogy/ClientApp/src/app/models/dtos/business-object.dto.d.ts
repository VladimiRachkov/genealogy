import { Metatype } from '../metatype';

export interface BusinessObjectInDto {
  id: string;
  name: string;
  title: string;
  metatype: Metatype;
  startDate: Date;
  finishDate: Date;
  isRemoved: boolean;
  data: string;
}

export interface BusinessObjectOutDto {
  id?: string;
  name?: string;
  title?: string;
  metatypeId?: string;
  isRemoved?: boolean;
  data?: string;
}

export interface BusinessObjectsCountInDto {
  count: number;
}
