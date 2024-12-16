export class GetHomeShowcaseDetailDTO {
  id!: string;
  productName!: string;
  brandName!: string;
  slug!: string;
  imageUrl!: string;
  currencyCode!: string;
  price!: number;
  priceWithoutDiscount!: number;
  avgStarRate!: number;
  commentCount!: number;
  favoriteId: string | null = null;
}
