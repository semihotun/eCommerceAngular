import { BaseEntity } from '../core/baseEntity';

export class DiscountProduct extends BaseEntity {
  discountId!: string;
  productStockId!: string;
  discountNumber!: number;
}
