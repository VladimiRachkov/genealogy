import { USER_STATUSES } from "@enums";

export interface User {
  id: string;
  email: string;
  username: string;
  password: string;
  firstName: string;
  lastName: string;
  finishDate: Date;
  startDate: Date;
  status: USER_STATUSES;
  roleId: string;
  token: string;
}
