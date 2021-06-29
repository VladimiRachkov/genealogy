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
    //Called before any other lifecycle hook. Use it to inject dependencies, but avoid any serious work here.
    //Add '${implements OnChanges}' to the class.
    console.log('ITEM', this.item)
  }

  onSelect(id: string) {}

  onRestore() {}

  onRemove() {}
}
