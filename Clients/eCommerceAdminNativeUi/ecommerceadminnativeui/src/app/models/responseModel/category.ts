export class Category {
  id!: string;
  categoryName!: string;
  parentCategoryId!: string | null;
}

export class CategoryTreeDTO {
  id!: string;
  categoryName!: string;
  subCategories!: CategoryTreeDTO[];
}
