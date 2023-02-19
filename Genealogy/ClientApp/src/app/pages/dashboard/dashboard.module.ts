import { NgModule } from '@angular/core';
import { DashboardComponent } from './dashboard.component';
import { CemeteryComponent } from './cemetery/cemetery.component';
import { Routes, RouterModule } from '@angular/router';
import { PagesComponent } from './pages/pages.component';
import { AngularEditorModule } from '@kolkov/angular-editor';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { SharedModule } from '@shared';
import { EditorComponent } from './pages/editor/editor.component';
import { LinkEditorComponent } from './pages/link-editor/link-editor.component';
import { MaterialModule } from 'app/material.module';
import { UsersComponent } from './users/users.component';
import { PersonComponent } from './person/person.component';
import { CatalogComponent } from './catalog/catalog.component';
import { MailComponent } from './mail/mail.component';
import { SettingsComponent } from './settings/settings.component';
import { NgSelectModule } from '@ng-select/ng-select';
import { CountyComponent } from './county/county.component';

const routes: Routes = [
  {
    path: '',
    component: DashboardComponent,
  },
];

@NgModule({
  imports: [
    SharedModule,
    RouterModule.forChild(routes),
    HttpClientModule,
    AngularEditorModule,
    FormsModule,
    CKEditorModule,
    MaterialModule,
    NgSelectModule
  ],
  declarations: [
    DashboardComponent,
    CemeteryComponent,
    CountyComponent,
    PersonComponent,
    PagesComponent,
    EditorComponent,
    LinkEditorComponent,
    UsersComponent,
    CatalogComponent,
    MailComponent,
    SettingsComponent,
  ],
})
export class DashboardModule {}
