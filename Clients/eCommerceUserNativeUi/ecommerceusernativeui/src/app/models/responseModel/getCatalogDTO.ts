export class GetCatalogDTO {
  categoryName!: string;
  categoryDescription!: string;
  catalogSpecificationDTOList!: CatalogSpecificationDTO[];
}

export class CatalogSpecificationDTO {
  specificationAttributeId!: string;
  specificationAttributeName!: string;
  specificationAttributeOptionList!: CatalogSpecificationOptionDTO[];
}

export class CatalogSpecificationOptionDTO {
  specificationAttributeOptionId!: string;
  specificationAttributeOptionName!: string;
}
