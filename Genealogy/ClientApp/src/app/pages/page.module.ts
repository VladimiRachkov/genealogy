import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StartComponent } from './start/start.component';
import { CatalogComponent } from './catalog/catalog.component';
import { NecropolisComponent } from './necropolis/necropolis.component';
import { GakoComponent } from './gako/gako.component';
import { NotifierService } from 'angular-notifier';
import { SharedModule } from '@shared';
import { MaterialModule } from 'app/material.module';
import { RouterModule } from '@angular/router';
import { PageViewerComponent } from './page-viewer/page-viewer.component';
import { LoginComponent } from './login';
import { ProfileComponent } from './profile/profile.component';
import { RegisterComponent } from './register';
import { PaymentComponent } from './payment/payment.component';

@NgModule({
  imports: [CommonModule, SharedModule, RouterModule, MaterialModule],
  exports: [StartComponent, CatalogComponent, NecropolisComponent, GakoComponent, PageViewerComponent, LoginComponent, RegisterComponent],
  declarations: [
    StartComponent,
    CatalogComponent,
    NecropolisComponent,
    GakoComponent,
    PageViewerComponent,
    LoginComponent,
    ProfileComponent,
    RegisterComponent,
    PaymentComponent,
  ],
  providers: [NotifierService],
})
export class PageModule {}
