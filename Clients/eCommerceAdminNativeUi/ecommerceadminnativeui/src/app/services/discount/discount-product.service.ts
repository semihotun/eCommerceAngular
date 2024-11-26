import { Injectable, inject } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpService } from '../core/http.service';
import { Result } from '../../models/core/result';
import { Destroyable } from '../../shared/destroyable.service';
import { ToastService } from '../core/toast.service';
import { DiscountProductStore } from '../../stores/discount-product.store';
import { DiscountProduct } from '../../models/responseModel/discount-product';

@Injectable({
  providedIn: 'root',
})
export class DiscountProductService extends Destroyable {
  constructor() {
    super();
  }
  http = inject(HttpService);
  discountProductStore = inject(DiscountProductStore);
  toast = inject(ToastService);
  getAllDiscountProduct(): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.get<Result<DiscountProduct[]>>(
        environment.baseUrl + 'discountProduct/getall',
        {},
        this.onDestroy,
        (response) => {
          this.discountProductStore.setDiscountProductList(response.data);
          resolve();
        },
        (err) => {
          reject();
        }
      );
    });
  }

  getDiscountProductById(id: string): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.get<Result<DiscountProduct>>(
        environment.baseUrl + 'discountProduct/getbyid',
        { id: id },
        this.onDestroy,
        (response) => {
          console.log(response.data);
          this.discountProductStore.setDiscountProduct(response.data);
          resolve();
        },
        (err) => {
          reject();
        }
      );
    });
  }
  creatediscountProduct(data: any): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<DiscountProduct>>(
        environment.baseUrl + 'discountProduct/create',
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
  updateDiscountProduct(data: any): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<DiscountProduct>>(
        environment.baseUrl + 'discountProduct/update',
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
