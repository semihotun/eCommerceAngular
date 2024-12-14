import { BaseEntity } from '../core/baseEntity';

export class CustomerUserAddress extends BaseEntity {
  name!: string;
  cityId!: string;
  districtId!: string;
  street!: string;
  address!: string;
}
