import { BaseEntity } from '../core/baseEntity';

export class SpecificationAttribute extends BaseEntity {
  name!: string;
  specificationAttributeOption!: SpecificationAttributeOption[];
}
export class SpecificationAttributeOption extends BaseEntity {
  specificationAttributeId!: string;
  name!: string;
}
