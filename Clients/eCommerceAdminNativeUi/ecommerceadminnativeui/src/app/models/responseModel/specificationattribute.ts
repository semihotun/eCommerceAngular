export class SpecificationAttribute {
  id!: string;
  name!: string;
  specificationAttributeOption!: SpecificationAttributeOption[];
}
export class SpecificationAttributeOption {
  id!: string;
  specificationAttributeId!: string;
  name!: string;
}
