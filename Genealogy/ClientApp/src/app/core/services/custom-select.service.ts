import { Injectable } from '@angular/core';
import { NgSelectConfig } from '@ng-select/ng-select';

@Injectable()
export class CustomSelectService {
  constructor(private config: NgSelectConfig) {
    this.config.notFoundText = 'Не найдено';
    this.config.appendTo = 'body';
  }
}
