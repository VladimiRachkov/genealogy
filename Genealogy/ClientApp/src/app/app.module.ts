import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ComponentModule } from './components/component.module';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ApiService } from './services/api.service';
import { ApiInterceptor } from './services/api.interceptor';
import { environment } from '@env/environment';
import { NgxsModule } from '@ngxs/store';
import { NgxsReduxDevtoolsPluginModule } from '@ngxs/devtools-plugin';
import { NgxsLoggerPluginModule } from '@ngxs/logger-plugin';
import { SharedModule } from '@shared/shared.module';
import { PersonState } from './states/person.state';
import { CemeteryState } from '@state/cemetery.state';
import { NotifierModule } from 'angular-notifier';
import { PageState } from '@state/page.state';
import { NgbModal, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AngularEditorModule } from '@kolkov/angular-editor';

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule.withServerTransition({ appId: 'genealogy' }),
    ComponentModule,
    AppRoutingModule,
    HttpClientModule,
    NgxsModule.forRoot([PersonState, CemeteryState, PageState], {
      developmentMode: !environment.production,
    }),
    NgxsLoggerPluginModule.forRoot(),
    NgxsReduxDevtoolsPluginModule.forRoot(),
    SharedModule,
    NotifierModule,
    NgbModule,
    AngularEditorModule
  ],
  exports: [],
  providers: [
    ApiService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ApiInterceptor,
      multi: true,
    },
    NgbModal,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
