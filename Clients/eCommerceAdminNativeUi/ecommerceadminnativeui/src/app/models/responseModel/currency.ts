import { BaseEntity } from '../core/baseEntity';

export class Currency extends BaseEntity {
  symbol!: string;
  code!: string;
  name!: string;
}
