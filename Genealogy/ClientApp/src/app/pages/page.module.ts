import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StartComponent } from './start/start.component';
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
import { FormsModule } from '@angular/forms';
import { NgSelectModule } from '@ng-select/ng-select';
import { CustomSelectService } from 'app/core/services/custom-select.service';
import { CountUpDirective } from 'app/directives/count-up.directive';

@NgModule({
  imports: [CommonModule, SharedModule, RouterModule, MaterialModule, FormsModule, NgSelectModule],
  exports: [StartComponent, NecropolisComponent, GakoComponent, PageViewerComponent, LoginComponent, RegisterComponent],
  declarations: [
    StartComponent,
    NecropolisComponent,
    GakoComponent,
    PageViewerComponent,
    LoginComponent,
    ProfileComponent,
    RegisterComponent,
    PaymentComponent,
    CountUpDirective
  ],
  providers: [NotifierService, CustomSelectService],
})
export class PageModule {}
