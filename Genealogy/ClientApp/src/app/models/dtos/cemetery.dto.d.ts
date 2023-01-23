export interface CemeteryDto {
  id?: string;
  name?: string;
  county?: CountyDto;
  isRemoved?: boolean;
  countyId?: string
}
