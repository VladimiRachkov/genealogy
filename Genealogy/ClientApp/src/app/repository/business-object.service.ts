import { HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiService } from '@core';
import { BusinessObjectInDto, BusinessObjectOutDto, BusinessObjectsCountInDto } from '@models';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class BusinessObjectService {
  constructor(private apiService: ApiService) {}

  url = 'businessobject';

  FetchBusinessObjectList(params: HttpParams): Observable<BusinessObjectInDto[]> {
    return this.apiService.get(this.url, params);
  }

  FetchBusinessObject(params: HttpParams): Observable<BusinessObjectInDto> {
    return this.apiService.get(this.url, params);
  }

  AddBusinessObject(body: BusinessObjectOutDto): Observable<BusinessObjectInDto> {
    return this.apiService.post<BusinessObjectInDto>(this.url, body);
  }

  UpdateBusinessObject(body: BusinessObjectOutDto): Observable<BusinessObjectInDto> {
    return this.apiService.put<BusinessObjectInDto>(this.url, body);
  }

  GetBusinessObjectsCount(params: HttpParams): Observable<BusinessObjectsCountInDto> {
    const url = this.url + '/count';
    return this.apiService.get<BusinessObjectsCountInDto>(url, params);
  }
}
