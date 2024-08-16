import { BaseEntity } from '../core/baseEntity';

export class Category extends BaseEntity {
  categoryName!: string;
  parentCategoryId!: string | null;
}

export class CategoryTreeDTO {
  id!: string;
  categoryName!: string;
  subCategories!: CategoryTreeDTO[];
}
