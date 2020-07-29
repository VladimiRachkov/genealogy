import { NgModule } from '@angular/core';
import { DashboardComponent } from './dashboard.component';
import { CemeteryComponent } from './cemetery/cemetery.component';
import { Routes, RouterModule } from '@angular/router';
import { NgxsModule } from '@ngxs/store';
import { environment } from '@env/environment';
import { GakoComponent } from './gako/gako.component';
import { PagesComponent } from './pages/pages.component';
import { EditorComponent } from './pages/editor/editor.component';
import { AngularEditorModule } from '@kolkov/angular-editor';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { SharedModule } from '@shared';

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
  ],
  declarations: [DashboardComponent, CemeteryComponent, GakoComponent, PagesComponent, EditorComponent],
})
export class DashboardModule {}
