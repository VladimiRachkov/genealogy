import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { Paginator } from '@models';

@Component({
  selector: 'lancet-paginator',
  templateUrl: './paginator.component.html',
  styleUrls: ['./paginator.component.scss'],
})
export class PaginatorComponent implements OnInit {
  @Input() options: Paginator;

  @Output() changePage: EventEmitter<number> = new EventEmitter();

  pageCount: number;
  pages: Array<number>;
  lastPageIndex: number;

  constructor() {}

  ngOnInit() {}

  ngOnChanges() {
    if (this.options) {
      const { count, step } = this.options;
      this.lastPageIndex = Math.floor(count / step);
      this.pageCount = this.lastPageIndex + 1;
      this.pages = new Array(this.pageCount);
    }
  }

  onChangePage(index: number) {
    if (index >= 0 && index <= this.lastPageIndex) {
      this.options.index = index;
      this.changePage.emit(index);
    }
  }

  onPrevPage() {
    if (this.options.index > 0) {
      this.options.index--;
      this.changePage.emit(this.options.index);
    }
  }

  onNextPage() {
    if (this.options.index < this.lastPageIndex) {
      this.options.index++;
      this.changePage.emit(this.options.index);
    }
  }
}
