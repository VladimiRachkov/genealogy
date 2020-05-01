import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PageModule } from './pages/page.module';
import { StartComponent } from './pages/start/start.component';
import { CatalogComponent } from './pages/catalog/catalog.component';

const routes: Routes = [
  {
    path: 'start',
    component: StartComponent,
  },
  {
    path: 'catalog',
    component: CatalogComponent,
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
