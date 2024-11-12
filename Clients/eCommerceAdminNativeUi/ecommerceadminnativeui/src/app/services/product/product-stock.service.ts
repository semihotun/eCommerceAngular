import { Injectable, inject } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpService } from '../core/http.service';
import { Result } from '../../models/core/result';
import { Destroyable } from '../../shared/destroyable.service';
import { ToastService } from '../core/toast.service';
import { ProductStock } from '../../models/responseModel/productStock';
import { ProductStockStore } from '../../stores/product-stock.store';
import { PagedList } from '../../models/core/grid';

@Injectable({
  providedIn: 'root',
})
export class ProductStockService extends Destroyable {
  constructor() {
    super();
  }
  http = inject(HttpService);
  productStockStore = inject(ProductStockStore);
  toast = inject(ToastService);
  getAllProductStock(): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.get<Result<ProductStock[]>>(
        environment.baseUrl + 'productStock/getall',
        {},
        this.onDestroy,
        (response) => {
          this.productStockStore.setProductStockList(response.data);
          resolve();
        },
        (err) => {
          reject();
        }
      );
    });
  }

  getProductStockById(id: string): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.get<Result<ProductStock>>(
        environment.baseUrl + 'productStock/getbyid',
        { id: id },
        this.onDestroy,
        (response) => {
          console.log(response.data);
          this.productStockStore.setProductStock(response.data);
          resolve();
        },
        (err) => {
          reject();
        }
      );
    });
  }
  createproductStock(data: any): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<ProductStock>>(
        environment.baseUrl + 'productStock/create',
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
  updateProductStock(data: any): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<ProductStock>>(
        environment.baseUrl + 'productStock/update',
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
  getProductStockGrid(data?: any): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<PagedList<any>>>(
        environment.baseUrl + 'productStock/getgrid',
        data,
        this.onDestroy,
        (response) => {
          this.productStockStore.setProductStockGrid(response.data);
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
