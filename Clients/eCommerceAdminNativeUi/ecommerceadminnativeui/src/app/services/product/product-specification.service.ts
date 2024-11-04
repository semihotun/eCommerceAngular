import { Injectable, inject } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpService } from '../core/http.service';
import { ToastService } from '../core/toast.service';
import { Destroyable } from 'src/app/shared/destroyable.service';
import { Result } from 'src/app/models/core/result';
import { PagedList } from 'src/app/models/core/grid';
import { ProductSpecificationStore } from 'src/app/stores/productSpecification.store';

@Injectable({
  providedIn: 'root',
})
export class ProductSpecificationService extends Destroyable {
  constructor() {
    super();
  }
  http = inject(HttpService);
  productSpecificationStore = inject(ProductSpecificationStore);
  toast = inject(ToastService);
  createproductSpecification(data: any): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<any>(
        environment.baseUrl + 'productSpecification/create',
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
  //Ürünün sahip olduğu spefikasyonlar
  getProductSpecificationGrid(data: any): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<PagedList<any>>>(
        environment.baseUrl + 'productSpecification/getgrid',
        data,
        this.onDestroy,
        (response) => {
          this.productSpecificationStore.setProductSpeficationGrid(
            response.data
          );
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
