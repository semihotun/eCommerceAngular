import { Injectable } from '@angular/core';
import { ComponentStore } from '@ngrx/component-store';
import { PagedList } from '../models/core/grid';
import { ProductFavoriteDTO } from '../models/responseModel/productFavoriteDTO';

export type ProductFavoriteState = {
  productFavoriteList: PagedList<ProductFavoriteDTO>;
};

export const productFavoriteInitialState: ProductFavoriteState = {
  productFavoriteList: new PagedList<ProductFavoriteDTO>(),
};

@Injectable({
  providedIn: 'root',
})
export class ProductFavoriteStore extends ComponentStore<ProductFavoriteState> {
  readonly productFavoriteList$ = this.selectSignal(
    (x) => x.productFavoriteList
  );

  constructor() {
    super(productFavoriteInitialState);
  }
  readonly addProductFavoriteList = this.updater(
    (state, productFavoriteList: PagedList<ProductFavoriteDTO>) => ({
      ...state,
      productFavoriteList: {
        ...productFavoriteList,
        data: [...state.productFavoriteList.data, ...productFavoriteList.data],
      },
    })
  );
  readonly resetProductFavoriteList = this.updater((state) => ({
    ...state,
    productFavoriteList: productFavoriteInitialState.productFavoriteList,
  }));
  readonly deleteProductFavoriteById = this.updater((state, id: string) => ({
    ...state,
    productFavoriteList: {
      ...state.productFavoriteList,
      data: state.productFavoriteList.data.filter(
        (item) => item.favoriteId !== id
      ),
    },
  }));
}
