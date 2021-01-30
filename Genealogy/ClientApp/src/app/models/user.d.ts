import { USER_STATUS } from "@enums";

export interface User {
  id: string;
  email: string;
  username: string;
  password: string;
  firstName: string;
  lastName: string;
  finishDate: Date;
  startDate: Date;
  status: USER_STATUS;
  roleId: string;
  token: string;
}
