import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { from } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  headers: HttpHeaders;
  constructor(private http: HttpClient) {}

  get<T>(controller: string, params: HttpParams) {
    return this.http.get<T>(`/api/${controller}`, { params });
  }

  post<T>(controller: string, body: any) {
    return this.http.post<T>(`/api/${controller}`, { ...body });
  }

  put<T>(controller: string, body: any) {
    return this.http.put<T>(`/api/${controller}`, { ...body });
  }

  delete<T>(controller: string, id: string) {
    return this.http.delete<T>(`/api/${controller}/${id}`);
  }

  restore<T>(controller: string, id: string) {
    return this.http.post<T>(`/api/${controller}/${id}/restore`, null);
  }
}
