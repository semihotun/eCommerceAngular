import { Injectable } from '@angular/core';
import { ComponentStore } from '@ngrx/component-store';
import { MailTemplateByIdDTO } from '../models/responseModel/mailTemplateByIdDTO';

export type MailTemplateState = {
  mailTemplate: MailTemplateByIdDTO;
};

export const mailTemplateInitialState: MailTemplateState = {
  mailTemplate: new MailTemplateByIdDTO(),
};

@Injectable({
  providedIn: 'root',
})
export class MailTemplateStore extends ComponentStore<MailTemplateState> {
  readonly mailTemplate$ = this.selectSignal((x) => x.mailTemplate);

  constructor() {
    super(mailTemplateInitialState);
  }

  readonly setMailTemplate = this.updater(
    (state, mailTemplate: MailTemplateByIdDTO) => ({
      ...state,
      mailTemplate,
    })
  );
}
