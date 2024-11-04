import { Injectable } from '@angular/core';
import { ComponentStore } from '@ngrx/component-store';
import { Product } from '../models/responseModel/product';

export type ProductState = {
  product: Product;
};

export const productInitialState: ProductState = {
  product: new Product(),
};

@Injectable({
  providedIn: 'root',
})
export class ProductStore extends ComponentStore<ProductState> {
  readonly product$ = this.selectSignal((x) => x.product);
  readonly productObservable = this.select((x) => x.product);

  constructor() {
    super(productInitialState);
  }

  readonly setProduct = this.updater((state, product: Product) => ({
    ...state,
    product,
  }));
}
