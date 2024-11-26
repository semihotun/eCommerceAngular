import { Injectable } from '@angular/core';
import { ComponentStore } from '@ngrx/component-store';
import { DiscountProduct } from '../models/responseModel/discount-product';

export type DiscountProductState = {
  discountProduct: DiscountProduct;
  discountProductList: DiscountProduct[];
};

export const discountProductInitialState: DiscountProductState = {
  discountProduct: new DiscountProduct(),
  discountProductList: [],
};

@Injectable({
  providedIn: 'root',
})
export class DiscountProductStore extends ComponentStore<DiscountProductState> {
  readonly discountProduct$ = this.selectSignal((x) => x.discountProduct);
  readonly discountProductList$ = this.selectSignal(
    (x) => x.discountProductList
  );
  constructor() {
    super(discountProductInitialState);
  }

  readonly setDiscountProduct = this.updater(
    (state, discountProduct: DiscountProduct) => ({
      ...state,
      discountProduct,
    })
  );

  readonly setDiscountProductList = this.updater(
    (state, discountProductList: DiscountProduct[]) => ({
      ...state,
      discountProductList,
    })
  );
}
