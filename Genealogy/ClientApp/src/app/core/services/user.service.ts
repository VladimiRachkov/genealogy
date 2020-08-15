import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '@env/environment';
import { User } from '@models';

@Injectable({ providedIn: 'root' })
export class UserService {
  constructor(private http: HttpClient) {}

  getAll() {
    return this.http.get<User[]>(`/api/user`);
  }
  register(user: User) {
    return this.http.post(`/api/user`, user);
  }
  delete(id: number) {
    return this.http.delete(`/api/user${id}`);
  }
}
