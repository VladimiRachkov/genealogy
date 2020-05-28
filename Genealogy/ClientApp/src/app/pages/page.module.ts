import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StartComponent } from './start/start.component';
import { CatalogComponent } from './catalog/catalog.component';
import { NecropolisComponent } from './necropolis/necropolis.component';
import { ArchiveComponent } from './archive/archive.component';
import { LoginComponent } from './login/login.component';

@NgModule({
  imports: [CommonModule],
  exports: [StartComponent, CatalogComponent, NecropolisComponent, ArchiveComponent, LoginComponent],
  declarations: [StartComponent, CatalogComponent, NecropolisComponent, ArchiveComponent, LoginComponent],
})
export class PageModule {}
