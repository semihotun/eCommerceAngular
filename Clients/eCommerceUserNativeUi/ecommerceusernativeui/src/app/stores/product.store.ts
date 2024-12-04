import { Injectable } from '@angular/core';
import { ProductDto } from '../models/responseModel/productDto';
import { ComponentStore } from '@ngrx/component-store';
import { PagedList } from '../models/core/grid';
import { ProductDetailSpeficationListDTO } from '../models/responseModel/productDetailSpeficationListDTO';

export type ProductState = {
  productDTO: ProductDto;
  selectedProductId: string;
  productSpecificationList: PagedList<ProductDetailSpeficationListDTO>;
};

export const productInitialState: ProductState = {
  productDTO: new ProductDto(),
  selectedProductId: '',
  productSpecificationList: new PagedList<ProductDetailSpeficationListDTO>(),
};

@Injectable({
  providedIn: 'root',
})
export class ProductStore extends ComponentStore<ProductState> {
  readonly productDTO$ = this.selectSignal((x) => x.productDTO);
  readonly selectedProductId$ = this.selectSignal((x) => x.selectedProductId);
  readonly productSpecificationList$ = this.selectSignal(
    (x) => x.productSpecificationList
  );
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
    selectedProductId: productDTO.id,
  }));
  readonly setProductSpecificationList = this.updater(
    (
      state,
      productSpecificationList: PagedList<ProductDetailSpeficationListDTO>
    ) => ({
      ...state,
      productSpecificationList,
    })
  );
}
