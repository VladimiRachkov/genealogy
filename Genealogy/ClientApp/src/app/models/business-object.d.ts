import { Metatype } from '@models';

export interface BusinessObject {
  id: string;
  name: string;
  title: string;
  metatype: Metatype;
  startDate: Date;
  finishDate: Date;
  isRemoved: boolean;
  data: string;
}

