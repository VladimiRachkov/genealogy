import { PaginatorOutDto } from "@models";

export interface PersonFilter extends PaginatorOutDto {
  id?: string;
  lastname?: string;
  fio?: string;
  cemeteryId?: string;
  countyId?: string;
}
