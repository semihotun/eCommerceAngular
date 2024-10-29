import { BaseEntity } from '../core/baseEntity';

export class Product extends BaseEntity {
  productName!: string;
  brandId!: string;
  categoryId!: string;
  productContent!: string;
  gtin!: string;
  sku!: string;
  productSeo!: string;
}
