import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Table } from '@models';

@Component({
  selector: 'app-table-row',
  templateUrl: './row.component.html',
  styleUrls: ['./row.component.scss'],
})
export class RowComponent {
  @Input() item: Table.Item;
  @Input() index: number;
  @Output() select: EventEmitter<string> = new EventEmitter();
}
