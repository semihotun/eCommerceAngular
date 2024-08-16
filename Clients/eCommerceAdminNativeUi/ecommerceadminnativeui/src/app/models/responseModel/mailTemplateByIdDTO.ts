import { BaseEntity } from '../core/baseEntity';

export class MailTemplateByIdDTO extends BaseEntity {
  templateHeader!: string;
  mailTemplateKeywordList: MailTemplateKeyword[] = [];
  templateContent!: string;
}
export class MailTemplateKeyword extends BaseEntity {
  mailTemplateId!: string;
  keyword!: string;
  description!: string;
}
