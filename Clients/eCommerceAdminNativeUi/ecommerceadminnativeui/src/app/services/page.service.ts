import { Injectable, inject } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpService } from './core/http.service';
import { Page } from '../models/responseModel/page';
import { Result } from '../models/core/result';
import { Destroyable } from '../shared/destroyable.service';
import { ToastService } from './core/toast.service';
import { PageStore } from '../stores/page.store';

@Injectable({
  providedIn: 'root',
})
export class PageService extends Destroyable {
  constructor() {
    super();
  }
  http = inject(HttpService);
  pageStore = inject(PageStore);
  toast = inject(ToastService);
  getPageById(id: string): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.get<Result<Page>>(
        environment.baseUrl + 'page/getbyid',
        { id: id },
        this.onDestroy,
        (response) => {
          this.pageStore.setPage(response.data);
          resolve();
        },
        (err) => {
          reject();
        }
      );
    });
  }
  createpage(data: any): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<Page>>(
        environment.baseUrl + 'page/create',
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
  updatePage(data: any): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<Page>>(
        environment.baseUrl + 'page/update',
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
