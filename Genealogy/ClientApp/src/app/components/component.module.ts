import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SideBarComponent } from './side-bar/side-bar.component';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { AppRoutingModule } from '../app-routing.module';

@NgModule({
  imports: [CommonModule, AppRoutingModule],
  exports: [SideBarComponent, NavBarComponent],
  declarations: [SideBarComponent, NavBarComponent]
})
export class ComponentModule {}
