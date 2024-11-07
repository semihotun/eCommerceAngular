import { inject, Injectable } from '@angular/core';
import { Destroyable } from 'src/app/shared/destroyable.service';
import { HttpService } from '../core/http.service';
import { ToastService } from '../core/toast.service';
import { Result } from 'src/app/models/core/result';
import { environment } from 'src/environments/environment';
import { ProductPhotoStore } from 'src/app/stores/product-photo.store';
import { ProductPhoto } from 'src/app/models/responseModel/productPhoto';
import { PagedList } from 'src/app/models/core/grid';

@Injectable({
  providedIn: 'root',
})
export class ProductPhotoService extends Destroyable {
  constructor() {
    super();
  }
  http = inject(HttpService);
  productPhotoStore = inject(ProductPhotoStore);
  toast = inject(ToastService);
  getProductPhotoByIdGrid(data: any): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<PagedList<any>>>(
        environment.baseUrl + 'productPhoto/getgrid',
        data,
        this.onDestroy,
        (response) => {
          this.productPhotoStore.setProductPhotoList(response.data);
          resolve();
        },
        (err) => {
          this.toast.presentDangerToast();
          reject();
        }
      );
    });
  }
  createProductPhoto(data: any): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<ProductPhoto>>(
        environment.baseUrl + 'productPhoto/create',
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
  updateProductPhoto(data: any): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<ProductPhoto>>(
        environment.baseUrl + 'productPhoto/update',
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
