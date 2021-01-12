import { NgModule } from '@angular/core';
import { DashboardComponent } from './dashboard.component';
import { CemeteryComponent } from './cemetery/cemetery.component';
import { Routes, RouterModule } from '@angular/router';
import { NgxsModule } from '@ngxs/store';
import { environment } from '@env/environment';
import { PagesComponent } from './pages/pages.component';
import { AngularEditorModule } from '@kolkov/angular-editor';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { SharedModule } from '@shared';
import { EditorComponent } from './pages/editor/editor.component';
import { LinkEditorComponent } from './pages/link-editor/link-editor.component';
import { MaterialModule } from 'app/material.module';
import { UsersComponent } from './users/users.component';
import { PersonComponent } from './person/person.component';

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
    NgxsModule.forRoot([], {
      developmentMode: !environment.production,
    }),
    HttpClientModule,
    AngularEditorModule,
    FormsModule,
    CKEditorModule,
    MaterialModule,
  ],
  declarations: [
    DashboardComponent,
    CemeteryComponent,
    PersonComponent,
    PagesComponent,
    EditorComponent,
    LinkEditorComponent,
    UsersComponent,
  ],
})
export class DashboardModule {}
