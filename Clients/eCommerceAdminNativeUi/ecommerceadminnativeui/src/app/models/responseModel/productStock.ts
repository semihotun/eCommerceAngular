import { BaseEntity } from '../core/baseEntity';

export class ProductStock extends BaseEntity {
  remainingStock!: number;
  totalStock!: number;
  warehouseId!: string;
  productId!: string;
}
