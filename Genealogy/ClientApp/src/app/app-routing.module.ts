import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PageModule } from './pages/page.module';
import { StartComponent } from './pages/start/start.component';
import { CatalogComponent } from './pages/catalog/catalog.component';
import { NecropolisComponent } from './pages/necropolis/necropolis.component';
import { ArchiveComponent } from './pages/archive/archive.component';
import { LoginComponent } from './pages/login/login.component';
import { DashboardModule } from './pages/dashboard/dashboard.module';

const routes: Routes = [
  {
    path: 'start',
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
    path: 'archive',
    component: ArchiveComponent,
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
    path: '*',
    component: StartComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule, PageModule],
  declarations: [],
})
export class AppRoutingModule {}
