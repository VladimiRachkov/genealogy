import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Table } from '@models';

@Component({
  selector: 'necropolis-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.scss'],
})
export class NecropolisTableComponent {
  @Input() data: Table.Data;
  @Input() selectedId: string;
  @Input() showRemoved: boolean = true;
  @Input() showActionButtons: boolean = false;
  @Input() startIndex: number = 0;

  @Output() change: EventEmitter<string> = new EventEmitter();
  @Output() remove: EventEmitter<string> = new EventEmitter();
  @Output() restore: EventEmitter<string> = new EventEmitter();

  ngOnChanges() {
    console.log(this.data);
  }

  onRemove(value: string) {
    event.stopPropagation();
    this.remove.emit(value);
  }

  onRestore(value: string) {
    this.restore.emit(value);
  }

  onSelect(value: string) {
    event.stopPropagation();
    this.change.emit(value);
  }

  onToggleRemoved() {
    this.showRemoved = !this.showRemoved;
  }
}
