import { ProductDto } from './productDto';

export class AllShowcaseDTO {
  id!: string;
  showCaseText!: string;
  showCaseTypeId!: string;
  showCaseTitle!: string;
  showCaseOrder!: number;
  showCaseProductList!: ProductDto[];
}
