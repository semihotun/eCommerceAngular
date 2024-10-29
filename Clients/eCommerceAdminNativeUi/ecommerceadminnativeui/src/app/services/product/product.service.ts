import { inject, Injectable } from '@angular/core';
import { Destroyable } from 'src/app/shared/destroyable.service';
import { HttpService } from '../core/http.service';
import { ToastService } from '../core/toast.service';
import { Result } from 'src/app/models/core/result';
import { environment } from 'src/environments/environment';
import { ProductStore } from 'src/app/stores/product.store';
import { Product } from 'src/app/models/responseModel/product';

@Injectable({
  providedIn: 'root',
})
export class ProductService extends Destroyable {
  constructor() {
    super();
  }
  http = inject(HttpService);
  productStore = inject(ProductStore);
  toast = inject(ToastService);
  getProductById(id: string): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.get<Result<Product>>(
        environment.baseUrl + 'product/getbyid',
        { id: id },
        this.onDestroy,
        (response) => {
          console.log(response.data);
          this.productStore.setProduct(response.data);
          resolve();
        },
        (err) => {
          reject();
        }
      );
    });
  }
  createProduct(data: any): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<Product>>(
        environment.baseUrl + 'product/create',
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
  updateProduct(data: any): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<Product>>(
        environment.baseUrl + 'product/update',
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
