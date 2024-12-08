export class CategoryTreeDTO {
  id!: string;
  categoryName!: string;
  slug!: string;
  subCategories: CategoryTreeDTO[] = [];
}
