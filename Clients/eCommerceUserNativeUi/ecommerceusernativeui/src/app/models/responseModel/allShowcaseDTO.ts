export class AllShowcaseDTO {
  id!: string;
  showCaseText!: string;
  showCaseTypeId!: string;
  showCaseTitle!: string;
  showCaseOrder!: number;
  showCaseProductList!: ShowCaseProductDto[];
}

export class ShowCaseProductDto {
  productName!: string;
  productSeo!: string;
  photoBase64!: string;
  currencyCode!: string;
  price!: number;
}