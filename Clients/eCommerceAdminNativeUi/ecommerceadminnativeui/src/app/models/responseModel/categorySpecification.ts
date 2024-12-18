import { BaseEntity } from '../core/baseEntity';

export interface CategorySpecification extends BaseEntity {
  categoryId: string;
  specificationAttributeteId: string;
}
