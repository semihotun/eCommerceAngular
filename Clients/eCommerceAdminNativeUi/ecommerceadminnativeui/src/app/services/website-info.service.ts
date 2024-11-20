import { Injectable, inject } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpService } from './core/http.service';
import { Result } from '../models/core/result';
import { ToastService } from './core/toast.service';
import { Destroyable } from '../shared/destroyable.service';
import { WebsiteInfoStore } from '../stores/website-info.store';
import { WebsiteInfo } from '../models/responseModel/websiteInfo';

@Injectable({
  providedIn: 'root',
})
export class WebSiteInfoService extends Destroyable {
  constructor() {
    super();
  }
  http = inject(HttpService);
  websiteInfoStore = inject(WebsiteInfoStore);
  toast = inject(ToastService);

  getWebsiteInfo(): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.get<Result<WebsiteInfo>>(
        environment.baseUrl + 'websiteInfo/get',
        {},
        this.onDestroy,
        (response) => {
          this.websiteInfoStore.setWebsiteInfo(response.data);
          resolve();
        },
        (err) => {
          reject();
        }
      );
    });
  }
  updateWebsiteInfo(data: any): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<WebsiteInfo>>(
        environment.baseUrl + 'websiteInfo/update',
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
