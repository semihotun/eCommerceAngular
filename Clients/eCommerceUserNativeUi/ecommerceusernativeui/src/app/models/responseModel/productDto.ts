export class ProductDto {
  id!: string;
  productName!: string;
  slug!: string;
  imageUrl!: string;
  currencyCode!: string;
  price!: number;
  priceWithoutDiscount!: number;
  brandName!: string;
  brandId!: string;
  categoryName!: string;
  categoryId!: string;
  productContent!: string;
  avgStarRate: number = 0;
  commentCount: number = 0;
  favoriteId!: string;
}
