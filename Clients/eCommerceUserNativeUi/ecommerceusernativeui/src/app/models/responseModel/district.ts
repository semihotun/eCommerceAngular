import { BaseEntity } from '../core/baseEntity';

export class District extends BaseEntity {
  name!: string;
  cityId!: string;
}
