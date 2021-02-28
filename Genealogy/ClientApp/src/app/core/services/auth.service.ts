import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { environment } from '@env/environment';
import { User } from '@models';
import { Store } from '@ngxs/store';
import { SetAdminMode, SetAuthorization } from '@actions';
import { Router } from '@angular/router';
import { StateResetAll } from 'ngxs-reset-plugin';
import { LocalStorage } from '../storage/interfaces/local-storage.interface';
import { BaseLocalStorage } from '../storage';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
  private currentUserSubject: BehaviorSubject<User>;
  public currentUser: Observable<User>;

  constructor(private http: HttpClient, private store: Store, private router: Router, private localStorage: LocalStorage) {
    this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(this.localStorage.getItem('currentUser')));
    this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserValue(): User {
    return this.currentUserSubject.value;
  }

  login(email: string, password: string) {
    return this.http
      .post<any>(`/api/user/auth`, { email, password })
      .pipe(
        map(user => {
          // store user details and jwt token in local storage to keep user logged in between page refreshes
          this.localStorage.setItem('currentUser', JSON.stringify(user));
          this.currentUserSubject.next(user);
          this.store.dispatch(new SetAuthorization(true));
          return user;
        })
      );
  }

  logout() {
    // remove user from local storage to log user out
    this.localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
    this.store.dispatch(new StateResetAll());
    this.router.navigate(['/']);
  }

  checkAdmin(): Observable<any> {
    const cookies = this.localStorage.getItem('currentUser');
    if (cookies) {
      const currentUser = JSON.parse(cookies);
      return this.http.get(`/api/user/checkadmin/${currentUser.id}`).pipe(tap(value => this.store.dispatch(new SetAdminMode(value))));
    }
    return new Observable();
  }

  getUserId(): string {
    const cookies = this.localStorage.getItem('currentUser');
    
    if (cookies) {
      const currentUser = JSON.parse(cookies);
      return currentUser.id;
    }

    return null;
  }
}
