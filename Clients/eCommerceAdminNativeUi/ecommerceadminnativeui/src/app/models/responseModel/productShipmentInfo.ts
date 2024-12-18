import { BaseEntity } from '../core/baseEntity';

export class ProductShipmentInfo extends BaseEntity {
  width!: number;
  length!: number;
  height!: number;
  weight!: number;
  productId!: string;
}
