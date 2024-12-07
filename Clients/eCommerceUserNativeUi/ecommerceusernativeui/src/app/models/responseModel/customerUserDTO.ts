import { BaseEntity } from '../core/baseEntity';

export class CustomerUserDTO extends BaseEntity {
  firstName!: string;
  lastName!: string;
  email!: string;
  createdOnUtc!: string;
  isActivationApprove!: boolean;
}
