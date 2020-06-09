import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Table } from './table';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.scss'],
})
export class TableComponent implements OnInit {
  @Input() data: Table.Data;
  @Input() selectedId: string;

  @Output() onChange: EventEmitter<string> = new EventEmitter();
  @Output() onRemove: EventEmitter<string> = new EventEmitter();
  constructor() {}

  ngOnInit() {}

  handleRemove(event, value) {
    console.log(typeof event);
    event.stopPropagation();
    this.onRemove.emit(value);
  }
}
