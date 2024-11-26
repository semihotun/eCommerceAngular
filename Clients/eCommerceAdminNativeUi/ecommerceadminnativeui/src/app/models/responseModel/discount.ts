import { BaseEntity } from '../core/baseEntity';

export class Discount extends BaseEntity {
  name!: string;
  discountTypeId!: string;
}
