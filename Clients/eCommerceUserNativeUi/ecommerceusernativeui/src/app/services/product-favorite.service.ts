import { inject, Injectable } from '@angular/core';
import { Destroyable } from 'src/app/shared/destroyable.service';
import { HttpService } from './core/http.service';
import { ToastService } from './core/toast.service';
import { Result } from '../models/core/result';
import { environment } from 'src/environments/environment';
import { ProductFavoriteStore } from '../stores/product-favorite.store';
import { PagedList } from '../models/core/grid';
import { ProductFavoriteDTO } from '../models/responseModel/productFavoriteDTO';
@Injectable({
  providedIn: 'root',
})
export class ProductFavoriteService extends Destroyable {
  constructor() {
    super();
  }
  http = inject(HttpService);
  toast = inject(ToastService);
  productFavoriteStore = inject(ProductFavoriteStore);
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
  getAllFavoriteProduct(pageIndex: number, pageSize: number): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<PagedList<ProductFavoriteDTO>>>(
        environment.baseUrl + 'productFavorite/getallfavoriteproduct',
        { PageIndex: pageIndex, PageSize: pageSize },
        this.onDestroy,
        (response) => {
          this.productFavoriteStore.addProductFavoriteList(response.data);
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
