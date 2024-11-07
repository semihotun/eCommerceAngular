import { BaseEntity } from '../core/baseEntity';

export class ProductPhoto extends BaseEntity {
  productId!: string;
  photoBase64!: string;
}
