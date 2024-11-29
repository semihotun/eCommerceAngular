import { Injectable } from '@angular/core';
import { ProductDto } from '../models/responseModel/productDto';
import { ComponentStore } from '@ngrx/component-store';

export type ProductState = {
  productDTO: ProductDto;
  selectedProductId: string;
};

export const productInitialState: ProductState = {
  productDTO: new ProductDto(),
  selectedProductId: '',
};

@Injectable({
  providedIn: 'root',
})
export class ProductStore extends ComponentStore<ProductState> {
  readonly productDTO$ = this.selectSignal((x) => x.productDTO);
  readonly selectedProductId$ = this.selectSignal((x) => x.selectedProductId);

  constructor() {
    super(productInitialState);
  }
  readonly setSelectedProductId = this.updater(
    (state, selectedProductId: string) => ({
      ...state,
      selectedProductId,
    })
  );
  readonly setProductDto = this.updater((state, productDTO: ProductDto) => ({
    ...state,
    productDTO,
  }));
}
