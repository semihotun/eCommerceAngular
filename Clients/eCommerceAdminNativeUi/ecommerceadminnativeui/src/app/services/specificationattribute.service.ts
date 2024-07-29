import { Injectable, inject } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpService } from './http.service';
import { Brand } from '../models/responseModel/brand';
import { Result } from '../models/core/result';
import { Destroyable } from '../shared/destroyable.service';
import { CreateBrand } from '../models/requestModel/createbrand';
import { ToastService } from './toast.service';
import { SpecificationAttributeStore } from '../stores/specificationattribute.store';
import { SpecificationAttribute } from '../models/responseModel/specificationattribute';

@Injectable({
  providedIn: 'root',
})
export class SpecificationAttributeService extends Destroyable {
  constructor() {
    super();
  }
  http = inject(HttpService);
  specificationAttributeStore = inject(SpecificationAttributeStore);
  toast = inject(ToastService);
  getSpecificationAttributeById(id: string): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.get<Result<SpecificationAttribute>>(
        environment.baseUrl +
          'specificationAttribute/getspecificationattributebyid',
        { id: id },
        this.onDestroy,
        (response) => {
          this.specificationAttributeStore.setSpecificationAttribute(
            response.data
          );
          resolve();
        },
        (err) => {
          reject();
        }
      );
    });
  }
  createSpecificationAttribute(data: CreateBrand): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<SpecificationAttribute>>(
        environment.baseUrl +
          'specificationattribute/createspecificationattribute',
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
  updateSpecificationAttribute(data: CreateBrand): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<Brand>>(
        environment.baseUrl +
          'specificationattribute/updatespecificationattribute',
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
