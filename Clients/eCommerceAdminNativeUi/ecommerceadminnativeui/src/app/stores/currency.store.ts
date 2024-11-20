import { Injectable } from '@angular/core';
import { ComponentStore } from '@ngrx/component-store';
import { Currency } from '../models/responseModel/currency';

export type CurrencyState = {
  currency: Currency;
  currencyList: Currency[];
};

export const currencyInitialState: CurrencyState = {
  currency: new Currency(),
  currencyList: [],
};

@Injectable({
  providedIn: 'root',
})
export class CurrencyStore extends ComponentStore<CurrencyState> {
  readonly currency$ = this.selectSignal((x) => x.currency);
  readonly currencyList$ = this.selectSignal((x) => x.currencyList);
  constructor() {
    super(currencyInitialState);
  }

  readonly setCurrency = this.updater((state, currency: Currency) => ({
    ...state,
    currency,
  }));

  readonly setCurrencyList = this.updater(
    (state, currencyList: Currency[]) => ({
      ...state,
      currencyList,
    })
  );
}
