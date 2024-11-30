import { Injectable } from '@angular/core';
import { ComponentStore } from '@ngrx/component-store';
import { PagedList } from '../models/core/grid';
import { ProductSpecificationGridDTO } from '../models/responseModel/productSpecificationGridDTO';

export type ProductSpecificationState = {
  productSpecificationList: PagedList<ProductSpecificationGridDTO>;
};

export const productSpecificationInitialState: ProductSpecificationState = {
  productSpecificationList: new PagedList<ProductSpecificationGridDTO>(),
};

@Injectable({
  providedIn: 'root',
})
export class ProductSpecificationStore extends ComponentStore<ProductSpecificationState> {
  readonly productSpecificationList$ = this.selectSignal(
    (x) => x.productSpecificationList
  );

  constructor() {
    super(productSpecificationInitialState);
  }
  readonly setProductSpecificationList = this.updater(
    (
      state,
      productSpecificationList: PagedList<ProductSpecificationGridDTO>
    ) => ({
      ...state,
      productSpecificationList,
    })
  );
}
