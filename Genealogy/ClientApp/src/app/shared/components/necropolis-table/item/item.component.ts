import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Table } from '@models';

@Component({
  selector: 'app-table-item',
  templateUrl: './item.component.html',
  styleUrls: ['./item.component.scss'],
})
export class ItemComponent {
  @Input() item: Table.Item;
  @Output() select: EventEmitter<string> = new EventEmitter();

  ngOnChanges(): void {

  }

  onSelect(id: string) {}

  onRestore() {}

  onRemove() {}
}
