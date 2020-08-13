import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AuthenticationService } from '../services';
import { NotifierService } from 'angular-notifier';
import { NOTIFICATIONS } from '@enums';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private notifierService: NotifierService, private authenticationService: AuthenticationService) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      catchError(err => {
        if (err instanceof HttpErrorResponse) {
          switch (err.status) {
            case 400:
              this.notifierService.notify('error', err.error, 'NOT_AUTHORIZED');
              break;
            case 401:
              this.notifierService.notify('error', NOTIFICATIONS.NOT_AUTHORIZED, 'NOT_AUTHORIZED');
              this.authenticationService.logout();
              break;
            case 500:
              this.notifierService.notify('error', NOTIFICATIONS.SERVER_ERROR, 'SERVER_ERROR');
              break;
          }
        }
        const error = err.error || err.statusText;
        return throwError(error);
      })
    );
  }
}
