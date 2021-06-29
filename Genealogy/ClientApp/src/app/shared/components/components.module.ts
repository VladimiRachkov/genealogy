import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { AssetsModule } from '../assets';
import { RouterModule } from '@angular/router';
import { FeedbackComponent, ModalComponent, PaginatorComponent } from '.';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { TableComponent } from './table/table.component';
import { NecropolisTableModule } from './necropolis-table/table.module';


@NgModule({
  imports: [CommonModule, AssetsModule, ReactiveFormsModule, FormsModule, RouterModule, NecropolisTableModule],
  exports: [TableComponent, PaginatorComponent, ModalComponent, FeedbackComponent, NecropolisTableModule],
  declarations: [PaginatorComponent, ModalComponent, FeedbackComponent, TableComponent],
  providers: [NgbModal],
})
export class ComponentsModule {}
