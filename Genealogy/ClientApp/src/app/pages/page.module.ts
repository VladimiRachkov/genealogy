import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StartComponent } from './start/start.component';
import { CatalogComponent } from './catalog/catalog.component';

@NgModule({
  imports: [CommonModule],
  exports: [StartComponent, CatalogComponent],
  declarations: [StartComponent, CatalogComponent],
})
export class PageModule {}
