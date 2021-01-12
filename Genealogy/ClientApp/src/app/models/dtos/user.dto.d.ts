import { USER_STATUSES } from "@enums";

export interface UserDto {
    id: string;
    email: string;
    username: string;
    firstName: string;
    lastName: string;
    finishDate: Date;
    startDate: Date;
    status: USER_STATUSES;
    roleId: string;
  }
