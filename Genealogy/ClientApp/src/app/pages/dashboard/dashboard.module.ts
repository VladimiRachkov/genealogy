import { NgModule } from '@angular/core';
import { DashboardComponent } from './dashboard.component';
import { CemeteryComponent } from './cemetery/cemetery.component';
import { Routes, RouterModule } from '@angular/router';
import { NgxsModule } from '@ngxs/store';
import { environment } from '@env/environment';
import { SharedModule } from '@shared/shared.module';
import { GakoComponent } from './gako/gako.component';
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
  ],
  declarations: [DashboardComponent, CemeteryComponent, GakoComponent],
})
export class DashboardModule {}
