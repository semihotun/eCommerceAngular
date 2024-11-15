import { BaseEntity } from '../core/baseEntity';

export class UserGroupRole extends BaseEntity {
  userGroupId!: string;
  roleId!: string;
}
