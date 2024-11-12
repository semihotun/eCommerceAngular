import { Injectable } from '@angular/core';
import { ComponentStore } from '@ngrx/component-store';
import { ProductStock } from '../models/responseModel/productStock';
import { ProductStockGridDTO } from '../models/responseModel/productStockGridDTO';
import { PagedList } from '../models/core/grid';

export type ProductStockState = {
  productStock: ProductStock;
  productStockList: ProductStock[];
  productStockGrid: PagedList<ProductStockGridDTO[]>;
};

export const productStockInitialState: ProductStockState = {
  productStock: new ProductStock(),
  productStockList: [],
  productStockGrid: new PagedList<ProductStockGridDTO[]>(),
};

@Injectable({
  providedIn: 'root',
})
export class ProductStockStore extends ComponentStore<ProductStockState> {
  readonly productStock$ = this.selectSignal((x) => x.productStock);
  readonly productStockList$ = this.selectSignal((x) => x.productStockList);
  readonly productStockGridObservable$ = this.select((x) => x.productStockGrid);
  constructor() {
    super(productStockInitialState);
  }

  readonly setProductStock = this.updater(
    (state, productStock: ProductStock) => ({
      ...state,
      productStock,
    })
  );

  readonly setProductStockList = this.updater(
    (state, productStockList: ProductStock[]) => ({
      ...state,
      productStockList,
    })
  );
  readonly setProductStockGrid = this.updater(
    (state, productStockGrid: PagedList<ProductStockGridDTO[]>) => ({
      ...state,
      productStockGrid,
    })
  );
}
