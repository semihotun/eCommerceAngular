import { Injectable, inject } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpService } from './core/http.service';
import { Result } from '../models/core/result';
import { Destroyable } from '../shared/destroyable.service';
import { ToastService } from './core/toast.service';
import { CurrencyStore } from '../stores/currency.store';
import { Currency } from '../models/responseModel/currency';

@Injectable({
  providedIn: 'root',
})
export class CurrencyService extends Destroyable {
  constructor() {
    super();
  }
  http = inject(HttpService);
  currencyStore = inject(CurrencyStore);
  toast = inject(ToastService);
  getAllCurrency(): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.get<Result<Currency[]>>(
        environment.baseUrl + 'currency/getall',
        {},
        this.onDestroy,
        (response) => {
          this.currencyStore.setCurrencyList(response.data);
          resolve();
        },
        (err) => {
          reject();
        }
      );
    });
  }

  getCurrencyById(id: string): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.get<Result<Currency>>(
        environment.baseUrl + 'currency/getbyid',
        { id: id },
        this.onDestroy,
        (response) => {
          console.log(response.data);
          this.currencyStore.setCurrency(response.data);
          resolve();
        },
        (err) => {
          reject();
        }
      );
    });
  }
  createcurrency(data: any): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<Currency>>(
        environment.baseUrl + 'currency/create',
        data,
        this.onDestroy,
        (response) => {
          this.toast.presentSuccessToast();
          resolve();
        },
        (err) => {
          this.toast.presentDangerToast(err.error.Message);
          reject();
        }
      );
    });
  }
  updateCurrency(data: any): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<Currency>>(
        environment.baseUrl + 'currency/update',
        data,
        this.onDestroy,
        (response) => {
          this.toast.presentSuccessToast();
          resolve();
        },
        (err) => {
          this.toast.presentDangerToast();
          reject();
        }
      );
    });
  }
}
