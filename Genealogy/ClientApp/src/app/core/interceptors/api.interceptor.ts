import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable()
export class ApiInterceptor implements HttpInterceptor {
  constructor(private spinner: NgxSpinnerService) {}
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    this.spinner.show();
    const authReq = req.clone({
      headers: req.headers,
    });
    return next.handle(authReq).pipe(
      tap(event => {
        this.spinner.hide();
        if (event instanceof HttpResponse) console.log('Server response', event);
      })
    );
  }
}
