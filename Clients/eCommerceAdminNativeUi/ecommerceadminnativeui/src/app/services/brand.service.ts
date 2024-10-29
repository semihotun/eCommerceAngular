import { Injectable, inject } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpService } from './core/http.service';
import { Brand } from '../models/responseModel/brand';
import { Result } from '../models/core/result';
import { Destroyable } from '../shared/destroyable.service';
import { BrandStore } from '../stores/brand.store';
import { ToastService } from './core/toast.service';

@Injectable({
  providedIn: 'root',
})
export class BrandService extends Destroyable {
  constructor() {
    super();
  }
  http = inject(HttpService);
  brandStore = inject(BrandStore);
  toast = inject(ToastService);
  getAllBrand(): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.get<Result<Brand[]>>(
        environment.baseUrl + 'brand/getall',
        {},
        this.onDestroy,
        (response) => {
          this.brandStore.setBrandList(response.data);
          resolve();
        },
        (err) => {
          reject();
        }
      );
    });
  }

  getBrandById(id: string): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.get<Result<Brand>>(
        environment.baseUrl + 'brand/getbyid',
        { id: id },
        this.onDestroy,
        (response) => {
          console.log(response.data);
          this.brandStore.setBrand(response.data);
          resolve();
        },
        (err) => {
          reject();
        }
      );
    });
  }
  createbrand(data: any): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<Brand>>(
        environment.baseUrl + 'brand/create',
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
  updateBrand(data: any): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<Brand>>(
        environment.baseUrl + 'brand/update',
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
