import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { from } from 'rxjs';
import { RequestOptions } from 'https';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  headers: HttpHeaders;
  constructor(private http: HttpClient) {}

  get<T>(controller: string, params: HttpParams) {
    return from(
      this.http.get<T>(`/api/${controller}`, { params })
    );
  }

  post<T>(controller: string, body: T) {
    return from(
      this.http.post<T>(`/api/${controller}`, { ...body })
    );
  }

  put<T>(controller: string, body: T) {
    return from(
      this.http.put<T>(`/api/${controller}`, { ...body })
    );
  }
}
