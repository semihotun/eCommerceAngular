import { inject, Injectable } from '@angular/core';
import { Destroyable } from 'src/app/shared/destroyable.service';
import { Result } from 'src/app/models/core/result';
import { environment } from 'src/environments/environment';
import { HttpService } from './core/http.service';
import { ToastService } from './core/toast.service';
import { GridPostData, PagedList } from '../models/core/grid';
import { ProductSpecificationStore } from '../stores/product-specification.store';

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
  getAllDataByProductId(productId: string, data: GridPostData): Promise<void> {
    const requestData = { ...data, productId: productId };
    return new Promise((resolve, reject) => {
      this.http.post<Result<PagedList<any>>>(
        environment.baseUrl + 'productSpecification/getgrid',
        requestData,
        this.onDestroy,
        (response) => {
          this.productSpecificationStore.setProductSpecificationList(
            response.data
          );
          resolve();
        },
        (err) => {
          reject();
        }
      );
    });
  }
}
