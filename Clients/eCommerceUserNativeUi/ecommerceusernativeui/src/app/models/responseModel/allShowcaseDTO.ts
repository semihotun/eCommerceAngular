export class AllShowcaseDTO {
  id!: string;
  showCaseText!: string;
  showCaseTypeId!: string;
  showCaseTitle!: string;
  showCaseOrder!: number;
  showCaseProductList!: HomeShowcaseProductDto[];
}
export class HomeShowcaseProductDto {
  id!: string;
  productName!: string;
  brandName!: string;
  slug!: string;
  photoBase64!: string;
  currencyCode!: string;
  price!: number;
  priceWithoutDiscount!: number;
  avgStarRate!: number;
  commentCount!: number;
  favoriteId: string | null = null;
}
