import { Injectable } from '@angular/core';
import { ComponentStore } from '@ngrx/component-store';
import { MailInfo } from '../models/responseModel/mailInfo';

export type MailInfoState = {
  mailInfo: MailInfo;
};

export const mailInfoInitialState: MailInfoState = {
  mailInfo: new MailInfo(),
};

@Injectable({
  providedIn: 'root',
})
export class MailInfoStore extends ComponentStore<MailInfoState> {
  readonly mailInfo$ = this.selectSignal((x) => x.mailInfo);
  constructor() {
    super(mailInfoInitialState);
  }

  readonly setMailInfo = this.updater((state, mailInfo: MailInfo) => ({
    ...state,
    mailInfo,
  }));
}
