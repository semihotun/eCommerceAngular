export class GetCatalogProductDTOBySlugRequest {
  categorySlug!: string;
  maxPrice!: number;
  minPrice!: number;
  specifications!: CatalogFilter[];
  pageIndex!: number;
  pageSize!: number;
}
export class CatalogSpecFilter {
  specificationAttributeId!: string;
  specificationAttributeOptionId!: string;
  specificationAttributeOptionName!: string;
}
export class CatalogFilter {
  specificationAttributeId!: string;
  specificationAttributeOptionId!: string;
  specificationAttributeOptionName!: string;
}
