import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PageModule } from './pages/page.module';
import { StartComponent } from './pages/start/start.component';
import { NecropolisComponent } from './pages/necropolis/necropolis.component';
import { GakoComponent } from './pages/gako/gako.component';
import { PageViewerComponent } from './pages/page-viewer/page-viewer.component';
import { LoginComponent } from './pages/login';
import { ROLES } from '@enums';
import { ProfileComponent } from './pages/profile/profile.component';
import { RegisterComponent } from './pages/register';
import { PaymentComponent } from './pages/payment/payment.component';
import { AuthGuard } from './core/guards';

const routes: Routes = [
  {
    path: '',
    component: StartComponent,
  },
  {
    path: 'necropolis',
    component: NecropolisComponent,
  },
  {
    path: 'catalog',
    loadChildren: () => import('./pages/catalog/catalog.module').then(m => m.CatalogModule),
  },
  {
    path: 'gako',
    component: GakoComponent,
  },
  {
    path: 'dashboard',
    loadChildren: () => import('./pages/dashboard/dashboard.module').then(m => m.DashboardModule),
    canActivate: [AuthGuard],
    data: { roles: [ROLES.ADMIN] },
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'register',
    component: RegisterComponent,
  },
  {
    path: ':parent/:child',
    component: PageViewerComponent,
  },
  {
    path: 'profile',
    component: ProfileComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'payment',
    component: PaymentComponent,
    canActivate: [AuthGuard],
  },
  {
    path: '**',
    component: StartComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule, PageModule],
  declarations: [],
})
export class AppRoutingModule {}
