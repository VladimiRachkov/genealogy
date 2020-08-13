import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { AssetsModule } from '../assets';
import { TableComponent } from './dashboard';
import { RouterModule } from '@angular/router';

@NgModule({
  imports: [CommonModule, AssetsModule, ReactiveFormsModule, FormsModule, RouterModule],
  exports: [TableComponent,],
  declarations: [TableComponent],
})
export class ComponentsModule {}
