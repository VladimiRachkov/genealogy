export interface User {
  id: string;
  email: string;
  username: string;
  password: string;
  firstName: string;
  lastName: string;
  finishDate: Date;
  startDate: Date;
  isConfirmed: boolean;
  roleId: string;
  token: string;
}
