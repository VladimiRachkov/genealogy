import { Cemetery } from './cemetery';

export interface Person {
  id: string;
  firstname: string;
  lastname: string;
  patronymic: string;
  cemetery?: Cemetery;
  source: string;
  startDate: string;
  finishDate: string;
  isRemoved: boolean;
}
