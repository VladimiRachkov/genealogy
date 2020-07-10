import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StartComponent } from './start/start.component';
import { CatalogComponent } from './catalog/catalog.component';
import { NecropolisComponent } from './necropolis/necropolis.component';
import { LoginComponent } from './login/login.component';
import { GakoComponent } from './gako/gako.component';
import { SharedModule } from '@shared/shared.module';
import { NotifierService } from 'angular-notifier';
import { SafeHtmlPipe } from '@shared/pipes/safehtml.pipe';

@NgModule({
  imports: [CommonModule, SharedModule],
  exports: [StartComponent, CatalogComponent, NecropolisComponent, GakoComponent, LoginComponent],
  declarations: [StartComponent, CatalogComponent, NecropolisComponent, GakoComponent, LoginComponent, SafeHtmlPipe],
  providers: [NotifierService]
})
export class PageModule {}
