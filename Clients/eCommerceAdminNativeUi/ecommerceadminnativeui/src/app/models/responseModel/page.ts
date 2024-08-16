import { BaseEntity } from '../core/baseEntity';

export class Page extends BaseEntity {
  pageTitle!: string;
  pageContent!: string;
  slug!: string;
}
