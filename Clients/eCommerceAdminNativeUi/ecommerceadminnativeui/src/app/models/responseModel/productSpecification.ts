import { BaseEntity } from '../core/baseEntity';

export class ProductSpecification extends BaseEntity {
  productId!: string;
  specificationAttributeOptionId!: string;
}
