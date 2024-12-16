import { inject, Injectable } from '@angular/core';
import { Destroyable } from 'src/app/shared/destroyable.service';
import { HttpService } from './core/http.service';
import { ToastService } from './core/toast.service';
import { Result } from '../models/core/result';
import { environment } from 'src/environments/environment';
import { ProductFavoriteStore } from '../stores/product-favorite.store';
import { PagedList } from '../models/core/grid';
import { ProductFavoriteDTO } from '../models/responseModel/productFavoriteDTO';
import { LoaderService } from './core/loader.service';
@Injectable({
  providedIn: 'root',
})
export class ProductFavoriteService extends Destroyable {
  constructor() {
    super();
  }
  http = inject(HttpService);
  toast = inject(ToastService);
  loader = inject(LoaderService);
  productFavoriteStore = inject(ProductFavoriteStore);
  createProductFavorite(data: any): Promise<string> {
    const url = environment.baseUrl + 'productFavorite/create';
    this.loader.addExcludedUrl(url);
    return new Promise((resolve, reject) => {
      this.http.post<Result<string>>(
        url,
        data,
        this.onDestroy,
        (response) => {
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
    const url = environment.baseUrl + 'productFavorite/delete';
    this.loader.addExcludedUrl(url);
    return new Promise((resolve, reject) => {
      this.http.post<Result<any>>(
        url,
        { id: id },
        this.onDestroy,
        (response) => {
          resolve();
        },
        (err) => {
          this.toast.presentDangerToast(err.error.message);
          reject();
        }
      );
    });
  }
  getAllFavoriteProduct(
    pageIndex: number,
    pageSize: number,
    addableNumber: number = pageSize
  ): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<PagedList<ProductFavoriteDTO>>>(
        environment.baseUrl + 'productFavorite/getallfavoriteproduct',
        { PageIndex: pageIndex, PageSize: pageSize },
        this.onDestroy,
        (response) => {
          const dataToAdd =
            addableNumber == pageSize
              ? response.data.data
              : response.data.data.slice(addableNumber);
          this.productFavoriteStore.addProductFavoriteList({
            ...response.data,
            data: dataToAdd,
          });
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
