import { Cemetery } from '@models';

export interface PersonDto {
  id?: string;
  firstname?: string;
  lastname?: string;
  patronymic?: string;
  cemetery?: Cemetery;
  source?: string;
  startDate?: string;
  finishDate?: string;
  isRemoved?: boolean;
  cemeteryId?: string;
  comment?: string;
}

export interface PersonOutDto {
  id: string;
}
