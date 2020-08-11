import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PageModule } from './pages/page.module';
import { StartComponent } from './pages/start/start.component';
import { CatalogComponent } from './pages/catalog/catalog.component';
import { NecropolisComponent } from './pages/necropolis/necropolis.component';
import { LoginComponent } from './pages/login/login.component';
import { GakoComponent } from './pages/gako/gako.component';
import { PageViewerComponent } from './pages/page-viewer/page-viewer.component';

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
    component: CatalogComponent,
  },
  {
    path: 'gako',
    component: GakoComponent,
  },
  {
    path: 'dashboard',
    loadChildren: () => import('./pages/dashboard/dashboard.module').then(m => m.DashboardModule),
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: ':parent/:child',
    component: PageViewerComponent,
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
