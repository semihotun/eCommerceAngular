import { Injectable } from '@angular/core';
import { ComponentStore } from '@ngrx/component-store';
import { Discount } from '../models/responseModel/discount';

export type DiscountState = {
  discount: Discount;
  discountList: Discount[];
};

export const discountInitialState: DiscountState = {
  discount: new Discount(),
  discountList: [],
};

@Injectable({
  providedIn: 'root',
})
export class DiscountStore extends ComponentStore<DiscountState> {
  readonly discount$ = this.selectSignal((x) => x.discount);
  readonly discountList$ = this.selectSignal((x) => x.discountList);
  constructor() {
    super(discountInitialState);
  }

  readonly setDiscount = this.updater((state, discount: Discount) => ({
    ...state,
    discount,
  }));

  readonly setDiscountList = this.updater(
    (state, discountList: Discount[]) => ({
      ...state,
      discountList,
    })
  );
}
