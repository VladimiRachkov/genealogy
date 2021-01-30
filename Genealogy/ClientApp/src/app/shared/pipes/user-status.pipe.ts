import { Pipe, PipeTransform } from '@angular/core';
import { USER_STATUS } from '@enums';

@Pipe({
  name: 'user-status'
})
export class UserStatusPipe implements PipeTransform {

  transform(value: string): any {
    return USER_STATUS[value];
  }

}
