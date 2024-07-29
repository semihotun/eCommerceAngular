import { Injectable } from '@angular/core';
import { ComponentStore } from '@ngrx/component-store';
import { Brand } from '../models/responseModel/brand';

export type BrandState = {
  brand: Brand;
};

export const brandInitialState: BrandState = {
  brand: new Brand(),
};

@Injectable({
  providedIn: 'root',
})
export class BrandStore extends ComponentStore<BrandState> {
  readonly brand$ = this.selectSignal((x) => x.brand);

  constructor() {
    super(brandInitialState);
  }

  readonly setBrand = this.updater((state, brand: Brand) => ({
    ...state,
    brand,
  }));
}
