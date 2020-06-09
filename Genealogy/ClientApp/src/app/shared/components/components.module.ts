import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TableComponent } from './dashboard/table/table.component';
import { AssetsModule } from '@shared/assets/assets.module';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  imports: [CommonModule, AssetsModule, ReactiveFormsModule],
  exports: [TableComponent],
  declarations: [TableComponent],
})
export class ComponentsModule {}
