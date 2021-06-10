import { PaginatorOutDto } from "../dtos";

export interface BusinessObjectFilter extends PaginatorOutDto {
  id?: string;
  name?: string;
  title?: string;
  metatypeId?: string;
  startDate?: Date;
  finishDate?: Date;
  isRemoved?: boolean;
  data?: string;
  userId?: string;
}
