import { inject, Injectable } from '@angular/core';
import { Destroyable } from '../shared/destroyable.service';
import { HttpService } from './core/http.service';
import { MailTemplateStore } from '../stores/mail-template.store';
import { Result } from '../models/core/result';
import { environment } from 'src/environments/environment';
import { ToastService } from './core/toast.service';
import { MailTemplateByIdDTO } from '../models/responseModel/mailTemplateByIdDTO';

@Injectable({
  providedIn: 'root',
})
export class MailTemplateService extends Destroyable {
  constructor() {
    super();
  }
  http = inject(HttpService);
  mailTemplateStore = inject(MailTemplateStore);
  toast = inject(ToastService);
  getMailTemplateById(id: string): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.get<Result<MailTemplateByIdDTO>>(
        environment.baseUrl + 'mailTemplate/getbyid',
        { id: id },
        this.onDestroy,
        (response) => {
          this.mailTemplateStore.setMailTemplate(response.data);
          resolve();
        },
        (err) => {
          reject();
        }
      );
    });
  }
  updateMailTemplate(data: any): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<any>>(
        environment.baseUrl + 'mailTemplate/update',
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
