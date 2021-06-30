import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { HTTPResponse } from '@models';
import { Observable, Subject } from 'rxjs';
import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root',
})
export class FileUploadService {
  constructor(private http: HttpClient) {}

  postFile(fileToUpload): Observable<HTTPResponse> {
    const endpoint = '/api/file';
    const formData: FormData = new FormData();

    formData.append('file', fileToUpload, fileToUpload.name);

    return this.http.post<HTTPResponse>(endpoint, formData, {});
  }
}

