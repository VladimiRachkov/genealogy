import { HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiService } from '@core';
import { BusinessObjectInDto, BusinessObjectOutDto, BusinessObjectsCountInDto } from '@models';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CatalogService {
  constructor(private apiService: ApiService) {}

  url = 'businessobject';

  FetchCatalogList(params: HttpParams): Observable<BusinessObjectInDto[]> {
    return this.apiService.get(this.url, params);
  }

  FetchCatalogItem(params: HttpParams): Observable<BusinessObjectInDto> {
    return this.apiService.get(this.url, params);
  }

  AddCatalogItem(body: BusinessObjectOutDto): Observable<BusinessObjectInDto> {
    return this.apiService.post<BusinessObjectInDto>(this.url, body);
  }

  UpdateCatalogItem(body: BusinessObjectOutDto): Observable<BusinessObjectInDto> {
    return this.apiService.put<BusinessObjectInDto>(this.url, body);
  }

  GetCatalogItemsCount(params: HttpParams): Observable<BusinessObjectsCountInDto> {
    const url = this.url + '/count';
    return this.apiService.get<BusinessObjectsCountInDto>(url, params);
  }
}
