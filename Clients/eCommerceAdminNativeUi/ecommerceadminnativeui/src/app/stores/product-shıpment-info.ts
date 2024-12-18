import { Injectable } from '@angular/core';
import { ComponentStore } from '@ngrx/component-store';
import { ProductShipmentInfo } from '../models/responseModel/productShipmentInfo';

export type ProductShipmentInfoState = {
  productShipmentInfo: ProductShipmentInfo;
  productShipmentInfoList: ProductShipmentInfo[];
};

export const productShipmentInfoInitialState: ProductShipmentInfoState = {
  productShipmentInfo: new ProductShipmentInfo(),
  productShipmentInfoList: [],
};

@Injectable({
  providedIn: 'root',
})
export class ProductShipmentInfoStore extends ComponentStore<ProductShipmentInfoState> {
  readonly productShipmentInfo$ = this.selectSignal(
    (x) => x.productShipmentInfo
  );
  readonly productShipmentInfoList$ = this.selectSignal(
    (x) => x.productShipmentInfoList
  );
  constructor() {
    super(productShipmentInfoInitialState);
  }

  readonly setProductShipmentInfo = this.updater(
    (state, productShipmentInfo: ProductShipmentInfo) => ({
      ...state,
      productShipmentInfo,
    })
  );

  readonly setProductShipmentInfoList = this.updater(
    (state, productShipmentInfoList: ProductShipmentInfo[]) => ({
      ...state,
      productShipmentInfoList,
    })
  );
}
