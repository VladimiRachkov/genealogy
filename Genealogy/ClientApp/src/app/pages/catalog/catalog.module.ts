import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CatalogComponent } from './catalog.component';
import { RouterModule, Routes } from '@angular/router';
import { InfoComponent } from './info/info.component';
import { PurchaseComponent } from './purchase/purchase.component';
import { SharedModule } from '@shared';

const routes: Routes = [
  {
    path: '',
    component: CatalogComponent,
  },
  {
    path: 'info',
    component: InfoComponent,
  },
  {
    path: '**',
    component: CatalogComponent,
  },
];

@NgModule({
  imports: [SharedModule, CommonModule, RouterModule.forChild(routes)],
  declarations: [CatalogComponent, InfoComponent, PurchaseComponent],
})
export class CatalogModule {}
