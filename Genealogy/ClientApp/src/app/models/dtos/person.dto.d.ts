import { Cemetery, PersonGroupDto } from '@models';
import { Person } from '../person';

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
  personGroup?: PersonGroupDto;
}

export interface PersonOutDto {
  id: string;
}
