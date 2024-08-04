export class MailTemplateByIdDTO {
  id!: string;
  templateHeader!: string;
  mailTemplateKeywordList: MailTemplateKeyword[] = [];
  templateContent!: string;
}
export class MailTemplateKeyword {
  id!: string;
  mailTemplateId!: string;
  keyword!: string;
  description!: string;
}
