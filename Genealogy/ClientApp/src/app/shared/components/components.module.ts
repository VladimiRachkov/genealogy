import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { AssetsModule } from '../assets';
import { RouterModule } from '@angular/router';
import { PaginatorComponent, TableComponent } from '.';

@NgModule({
  imports: [CommonModule, AssetsModule, ReactiveFormsModule, FormsModule, RouterModule],
  exports: [TableComponent, PaginatorComponent],
  declarations: [TableComponent, PaginatorComponent],
})
export class ComponentsModule {}
