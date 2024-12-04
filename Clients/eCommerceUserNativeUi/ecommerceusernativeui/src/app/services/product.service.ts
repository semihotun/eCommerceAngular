import { inject, Injectable } from '@angular/core';
import { Destroyable } from 'src/app/shared/destroyable.service';
import { Result } from 'src/app/models/core/result';
import { environment } from 'src/environments/environment';
import { ProductStore } from 'src/app/stores/product.store';
import { ProductDto } from '../models/responseModel/productDto';
import { HttpService } from './core/http.service';
import { ToastService } from './core/toast.service';
import { PagedList } from '../models/core/grid';
import { GetProductDetailSpeficationListRequest } from '../models/requestModel/getProductDetailSpeficationListRequest';
import { ProductDetailSpeficationListDTO } from '../models/responseModel/productDetailSpeficationListDTO';

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
  getProductDtoById(id: string): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.get<Result<ProductDto>>(
        environment.baseUrl + 'product/getproductdetailbyid',
        { id: id },
        this.onDestroy,
        (response) => {
          this.productStore.setProductDto(response.data);
          resolve();
        },
        (err) => {
          reject();
        }
      );
    });
  }
  getProductDtoBySlug(slug: string): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.get<Result<ProductDto>>(
        environment.baseUrl + 'product/getproductdetailbyslug',
        { slug: slug },
        this.onDestroy,
        (response) => {
          this.productStore.setProductDto(response.data);
          resolve();
        },
        (err) => {
          reject();
        }
      );
    });
  }
  getProductDetailSpeficationList(
    data: GetProductDetailSpeficationListRequest
  ): Promise<void> {
    return new Promise((resolve, reject) => {
      const url =
        environment.baseUrl +
        'productSpecification/getproductdetailspecificationlist';
      this.http.post<Result<PagedList<any>>>(
        url,
        data,
        this.onDestroy,
        (response) => {
          this.productStore.setProductSpecificationList(response.data);
          resolve();
        },
        (err) => {
          console.log(err);
          reject();
        }
      );
    });
  }
}
