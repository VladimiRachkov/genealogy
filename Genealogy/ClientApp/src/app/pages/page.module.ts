import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StartComponent } from './start/start.component';
import { CatalogComponent } from './catalog/catalog.component';
import { NecropolisComponent } from './necropolis/necropolis.component';
import { LoginComponent } from './login/login.component';
import { GakoComponent } from './gako/gako.component';
import { NotifierService } from 'angular-notifier';
import { SharedModule } from '@shared';
import { ShowComponent } from './show/show.component';

@NgModule({
  imports: [CommonModule, SharedModule],
  exports: [StartComponent, CatalogComponent, NecropolisComponent, GakoComponent, LoginComponent, ShowComponent],
  declarations: [StartComponent, CatalogComponent, NecropolisComponent, GakoComponent, LoginComponent, ShowComponent],
  providers: [NotifierService],
})
export class PageModule {}
