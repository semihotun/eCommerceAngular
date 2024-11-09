import { inject, Injectable } from '@angular/core';
import { HttpService } from '../core/http.service';
import { ShowcaseStore } from 'src/app/stores/showcase.store';
import { ToastService } from '../core/toast.service';
import { Destroyable } from 'src/app/shared/destroyable.service';
import { ShowCaseProduct } from 'src/app/models/responseModel/showcaseProduct';
import { Result } from 'src/app/models/core/result';
import { environment } from 'src/environments/environment';
import { PagedList } from 'src/app/models/core/grid';

@Injectable({
  providedIn: 'root',
})
export class ShowcaseProductService extends Destroyable {
  http = inject(HttpService);
  showcaseStore = inject(ShowcaseStore);
  toast = inject(ToastService);
  constructor() {
    super();
  }
  createShowcaseProduct(data: any): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<ShowCaseProduct>>(
        environment.baseUrl + 'showCaseProduct/create',
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
  getShowCaseProductGrid(data?: any): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<PagedList<any>>>(
        environment.baseUrl + 'showCaseProduct/getgrid',
        data,
        this.onDestroy,
        (response) => {
          this.showcaseStore.setShowcaseProductGrid(response.data);
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
