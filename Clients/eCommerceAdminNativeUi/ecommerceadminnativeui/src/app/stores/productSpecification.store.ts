import { Injectable } from '@angular/core';
import { ComponentStore } from '@ngrx/component-store';
import { ProductSpecificationGridDTO } from '../models/responseModel/productSpecificationGridDTO';
import { PagedList } from './../models/core/grid';

export type ProductSpecificationState = {
  productSpeficationGrid: PagedList<ProductSpecificationGridDTO[]>;
};

export const productSpecificationInitialState: ProductSpecificationState = {
  productSpeficationGrid: new PagedList<[]>(),
};

@Injectable({
  providedIn: 'root',
})
export class ProductSpecificationStore extends ComponentStore<ProductSpecificationState> {
  readonly productSpeficationGrid$ = this.selectSignal(
    (x) => x.productSpeficationGrid
  );
  readonly productSpeficationObservable$ = this.select(
    (x) => x.productSpeficationGrid
  );
  constructor() {
    super(productSpecificationInitialState);
  }
  readonly setProductSpeficationGrid = this.updater(
    (
      state,
      productSpeficationGrid: PagedList<ProductSpecificationGridDTO[]>
    ) => ({
      ...state,
      productSpeficationGrid,
    })
  );
}
