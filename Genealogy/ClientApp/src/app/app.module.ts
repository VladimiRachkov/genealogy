import { BrowserModule } from '@angular/platform-browser';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ComponentModule } from './components/component.module';
import { HttpClientModule } from '@angular/common/http';
import { environment } from '@env/environment';
import { NgxsModule } from '@ngxs/store';
import { NgxsReduxDevtoolsPluginModule } from '@ngxs/devtools-plugin';
import { NgbModal, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AngularEditorModule } from '@kolkov/angular-editor';
import { PersonState, CemeteryState, PageState, MainState, UserState, MetatypeState, CatalogState, SettingState, CountyState } from '@states';
import { SharedModule } from '@shared';
import { NotifierModule } from 'angular-notifier';
import { NgxSpinnerModule } from 'ngx-spinner';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LinkState } from './states/link.state';
import { NgxsResetPluginModule } from 'ngxs-reset-plugin';
import { CoreModule } from './core/core.module';
import { MailState } from './states/mail.state';

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule.withServerTransition({ appId: 'genealogy' }),
    ComponentModule,
    AppRoutingModule,
    HttpClientModule,
    NgxsModule.forRoot([MainState, PersonState, CemeteryState, PageState, LinkState, UserState, MetatypeState, CatalogState, MailState, SettingState, CountyState], {
      developmentMode: !environment.production,
    }),
    NgxsResetPluginModule.forRoot(),
    //NgxsLoggerPluginModule.forRoot(),
    NgxsReduxDevtoolsPluginModule.forRoot(),
    SharedModule,
    NotifierModule,
    NgbModule,
    AngularEditorModule,
    NgxSpinnerModule,
    BrowserAnimationsModule,
    CoreModule,
  ],
  exports: [],
  providers: [NgbModal],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class AppModule {}
