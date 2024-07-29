import { Injectable, inject } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpService } from './http.service';
import { Brand } from '../models/responseModel/brand';
import { Result } from '../models/core/result';
import { Destroyable } from '../shared/destroyable.service';
import { CreateBrand } from '../models/requestModel/createbrand';
import { BrandStore } from '../stores/brand.store';
import { ToastService } from './toast.service';

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
  getBrandById(id: string): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.get<Result<Brand>>(
        environment.baseUrl + 'brand/getbrandbyid',
        { id: id },
        this.onDestroy,
        (response) => {
          this.brandStore.setBrand(response.data);
          resolve();
        },
        (err) => {
          reject();
        }
      );
    });
  }
  createbrand(data: CreateBrand): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<Brand>>(
        environment.baseUrl + 'brand/createbrand',
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
  updateBrand(data: CreateBrand): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<Brand>>(
        environment.baseUrl + 'brand/updatebrand',
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
