import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { AssetsModule } from '../assets';
import { RouterModule } from '@angular/router';
import { ModalComponent, PaginatorComponent, TableComponent } from '.';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  imports: [CommonModule, AssetsModule, ReactiveFormsModule, FormsModule, RouterModule],
  exports: [TableComponent, PaginatorComponent, ModalComponent],
  declarations: [TableComponent, PaginatorComponent, ModalComponent],
  providers: [NgbModal],
})
export class ComponentsModule {}
