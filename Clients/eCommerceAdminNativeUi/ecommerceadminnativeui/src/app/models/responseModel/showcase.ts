import { BaseEntity } from '../core/baseEntity';

export class ShowCase extends BaseEntity {
  showCaseOrder!: number;
  showCaseTitle!: string;
  showCaseTypeId!: string;
  showCaseText!: string;
}
