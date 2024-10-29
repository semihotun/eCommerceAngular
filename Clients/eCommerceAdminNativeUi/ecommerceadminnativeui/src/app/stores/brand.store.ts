import { Injectable } from '@angular/core';
import { ComponentStore } from '@ngrx/component-store';
import { Brand } from '../models/responseModel/brand';

export type BrandState = {
  brand: Brand;
  brandList: Brand[];
};

export const brandInitialState: BrandState = {
  brand: new Brand(),
  brandList: [],
};

@Injectable({
  providedIn: 'root',
})
export class BrandStore extends ComponentStore<BrandState> {
  readonly brand$ = this.selectSignal((x) => x.brand);
  readonly brandList$ = this.selectSignal((x) => x.brandList);
  constructor() {
    super(brandInitialState);
  }

  readonly setBrand = this.updater((state, brand: Brand) => ({
    ...state,
    brand,
  }));

  readonly setBrandList = this.updater((state, brandList: Brand[]) => ({
    ...state,
    brandList,
  }));
}
