import { inject, Injectable } from '@angular/core';
import { Destroyable } from 'src/app/shared/destroyable.service';
import { HttpService } from './core/http.service';
import { ToastService } from './core/toast.service';
import { Result } from '../models/core/result';
import { environment } from 'src/environments/environment';
@Injectable({
  providedIn: 'root',
})
export class ProductFavoriteService extends Destroyable {
  constructor() {
    super();
  }
  http = inject(HttpService);
  toast = inject(ToastService);
  createProductFavorite(data: any): Promise<string> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<string>>(
        environment.baseUrl + 'productFavorite/create',
        data,
        this.onDestroy,
        (response) => {
          this.toast.presentSuccessToast();
          resolve(response.data);
        },
        (err) => {
          this.toast.presentDangerToast(err.error.message);
          reject();
        }
      );
    });
  }
  deleteProductFavorite(id: string): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<any>>(
        environment.baseUrl + 'productFavorite/delete',
        { id: id },
        this.onDestroy,
        (response) => {
          this.toast.presentSuccessToast();
          resolve();
        },
        (err) => {
          this.toast.presentDangerToast(err.error.message);
          reject();
        }
      );
    });
  }
}
