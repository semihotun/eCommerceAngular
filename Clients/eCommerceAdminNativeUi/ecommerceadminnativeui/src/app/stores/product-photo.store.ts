import { Injectable } from '@angular/core';
import { ComponentStore } from '@ngrx/component-store';
import { Product } from '../models/responseModel/product';
import { ProductPhoto } from '../models/responseModel/productPhoto';
import { PagedList } from '../models/core/grid';

export type ProductPhotoState = {
  productPhotoByProductIdList: PagedList<ProductPhoto[]>;
};

export const productInitialState: ProductPhotoState = {
  productPhotoByProductIdList: new PagedList<ProductPhoto[]>(),
};

@Injectable({
  providedIn: 'root',
})
export class ProductPhotoStore extends ComponentStore<ProductPhotoState> {
  readonly productPhotoByProductIdList$ = this.selectSignal(
    (x) => x.productPhotoByProductIdList
  );
  readonly productPhotoByProductIdListObservable$ = this.select(
    (x) => x.productPhotoByProductIdList
  );

  constructor() {
    super(productInitialState);
  }

  readonly setProductPhotoList = this.updater(
    (state, productPhotoByProductIdList: PagedList<ProductPhoto[]>) => ({
      ...state,
      productPhotoByProductIdList,
    })
  );
}
