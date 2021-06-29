import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NecropolisTableComponent } from './table.component';
import { RowComponent } from './row/row.component';
import { ItemComponent } from './item/item.component';
import { AssetsModule } from 'app/shared/assets';

@NgModule({
  imports: [CommonModule, RouterModule, AssetsModule],
  exports: [NecropolisTableComponent, RowComponent, ItemComponent],
  declarations: [NecropolisTableComponent, RowComponent, ItemComponent],
  providers: [NgbModal],
})
export class NecropolisTableModule {}
