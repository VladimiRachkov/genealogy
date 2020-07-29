import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { AuthFormComponent } from './auth-form/auth-form.component';
import { AssetsModule } from '../assets';
import { TableComponent } from './dashboard';

@NgModule({
  imports: [CommonModule, AssetsModule, ReactiveFormsModule, FormsModule],
  exports: [TableComponent, AuthFormComponent],
  declarations: [TableComponent, AuthFormComponent],
})
export class ComponentsModule {}
