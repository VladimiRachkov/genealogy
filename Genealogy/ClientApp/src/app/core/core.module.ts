import { NgModule } from '@angular/core';
import { StorageModule, BaseLocalStorage } from './storage';
import { ApiService, ApiInterceptor, AuthenticationService } from './services';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { JwtInterceptor, ErrorInterceptor } from './interceptors';

@NgModule({
  declarations: [],
  imports: [StorageModule.forRoot()],
  exports: [],
  providers: [
    ApiService,
    AuthenticationService,
    { provide: HTTP_INTERCEPTORS, useClass: ApiInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
  ],
})
export class CoreModule {}
