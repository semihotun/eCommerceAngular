import { Injectable, inject } from '@angular/core';
import { Destroyable } from 'src/app/shared/destroyable.service';
import { environment } from 'src/environments/environment';
import { HttpService } from '../core/http.service';
import { ProductShipmentInfoStore } from 'src/app/stores/product-shıpment-info';
import { ToastService } from '../core/toast.service';
import { ProductShipmentInfo } from 'src/app/models/responseModel/productShipmentInfo';
import { Result } from 'src/app/models/core/result';

@Injectable({
  providedIn: 'root',
})
export class ProductShipmentInfoService extends Destroyable {
  constructor() {
    super();
  }
  http = inject(HttpService);
  productShipmentInfoStore = inject(ProductShipmentInfoStore);
  toast = inject(ToastService);

  getProductShipmentInfoByProductId(
    productId: string
  ): Promise<ProductShipmentInfo> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<ProductShipmentInfo>>(
        environment.baseUrl +
          'productShipmentInfo/getproductshipmentinfobyproductid',
        { productId: productId },
        this.onDestroy,
        (response) => {
          console.log(response.data);
          this.productShipmentInfoStore.setProductShipmentInfo(response.data);
          resolve(response.data);
        },
        (err) => {
          reject();
        }
      );
    });
  }
  createproductShipmentInfo(data: any): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<ProductShipmentInfo>>(
        environment.baseUrl + 'productShipmentInfo/create',
        data,
        this.onDestroy,
        (response) => {
          this.toast.presentSuccessToast();
          this.productShipmentInfoStore.setProductShipmentInfo(response.data);
          resolve();
        },
        (err) => {
          this.toast.presentDangerToast();
          reject();
        }
      );
    });
  }
  updateProductShipmentInfo(data: any): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<ProductShipmentInfo>>(
        environment.baseUrl + 'productShipmentInfo/update',
        data,
        this.onDestroy,
        (response) => {
          this.toast.presentSuccessToast();
          this.productShipmentInfoStore.setProductShipmentInfo(response.data);
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
