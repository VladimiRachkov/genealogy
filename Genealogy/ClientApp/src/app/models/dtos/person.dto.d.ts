import { Cemetery } from '@mdl/cemetery';

export interface PersonDto {
  id?: string;
  firstname?: string;
  lastname?: string;
  patronymic?: string;
  cemetery?: Cemetery;
  source?: string;
  startDate?: string;
  finishDate?: string;
  removed?: boolean;
  cemeteryId?: string;
}
