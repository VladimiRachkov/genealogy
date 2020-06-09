import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

@Injectable()
export class ApiInterceptor implements HttpInterceptor {
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    req.headers.set('Content-Type', 'application/json');
    req.headers.set('Session', '123456789');

    const authReq = req.clone({ 
      headers: req.headers,
    });
    return next.handle(authReq).pipe(
      tap(
        event => {
          if (event instanceof HttpResponse) console.log('Server response', event);
        },
        err => {
          if (err instanceof HttpErrorResponse) {
            if (err.status == 401) console.log('Unauthorized');
          }
        }
      )
    );
  }
}
