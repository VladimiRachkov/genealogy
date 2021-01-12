import { Pipe, PipeTransform } from '@angular/core';
import { USER_STATUSES } from '@enums';

@Pipe({
  name: 'user-status'
})
export class UserStatusPipe implements PipeTransform {

  transform(value: string): any {
    console.log(value, USER_STATUSES)
    return USER_STATUSES[value];
  }

}
