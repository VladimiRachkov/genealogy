import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { NotifierService } from 'angular-notifier';
import { NOTIFICATIONS } from '@enums';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable()
export class ApiInterceptor implements HttpInterceptor {
  constructor(private notifierService: NotifierService, private spinner: NgxSpinnerService) {}
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    req.headers.set('Content-Type', 'application/json');
    req.headers.set('Session', '123456789');
    this.spinner.show();
    const authReq = req.clone({
      headers: req.headers,
    });
    return next.handle(authReq).pipe(
      tap(
        event => {
          this.spinner.hide();
          if (event instanceof HttpResponse) console.log('Server response', event);
        },
        err => {
          if (err instanceof HttpErrorResponse) {
            switch (err.status) {
              case 400:
                this.notifierService.notify('error', err.error, 'NOT_AUTHORIZED');
                break;
              case 401:
                this.notifierService.notify('error', NOTIFICATIONS.NOT_AUTHORIZED, 'NOT_AUTHORIZED');
                break;
              case 500:
                this.notifierService.notify('error', NOTIFICATIONS.SERVER_ERROR, 'SERVER_ERROR');
                break;
            }
          }
        }
      )
    );
  }
}
