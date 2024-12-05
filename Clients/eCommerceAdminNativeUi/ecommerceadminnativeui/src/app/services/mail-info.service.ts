import { Injectable, inject } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpService } from './core/http.service';
import { Result } from '../models/core/result';
import { ToastService } from './core/toast.service';
import { Destroyable } from '../shared/destroyable.service';
import { MailInfo } from '../models/responseModel/mailInfo';
import { MailInfoStore } from '../stores/mail-info.store';

@Injectable({
  providedIn: 'root',
})
export class MailInfoService extends Destroyable {
  constructor() {
    super();
  }
  http = inject(HttpService);
  mailInfoStore = inject(MailInfoStore);
  toast = inject(ToastService);

  getMailInfo(): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.get<Result<MailInfo>>(
        environment.baseUrl + 'mailInfo/get',
        {},
        this.onDestroy,
        (response) => {
          this.mailInfoStore.setMailInfo(response.data);
          resolve();
        },
        (err) => {
          reject();
        }
      );
    });
  }
  updateMailInfo(data: any): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<MailInfo>>(
        environment.baseUrl + 'mailInfo/update',
        data,
        this.onDestroy,
        (response) => {
          this.toast.presentSuccessToast();
          resolve();
        },
        (err) => {
          this.toast.presentDangerToast();
          reject();
        }
      );
    });
  }
}
