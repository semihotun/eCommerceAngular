import { Injectable, inject } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpService } from '../core/http.service';
import { Brand } from '../../models/responseModel/brand';
import { Result } from '../../models/core/result';
import { Destroyable } from '../../shared/destroyable.service';
import { ToastService } from '../core/toast.service';
import { SpecificationAttribute } from '../../models/responseModel/specificationattribute';
import { SpecificationAttributeStore } from 'src/app/stores/specificationattribute.store';

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
        environment.baseUrl + 'specificationAttribute/getbyid',
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
  getAllSpecificationAttribute(): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.get<Result<SpecificationAttribute[]>>(
        environment.baseUrl + 'specificationAttribute/getall',
        {},
        this.onDestroy,
        (response) => {
          this.specificationAttributeStore.setSpecificationAttributeList(
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
  createSpecificationAttribute(data: any): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<SpecificationAttribute>>(
        environment.baseUrl + 'specificationattribute/create',
        data,
        this.onDestroy,
        (response) => {
          this.specificationAttributeStore.setSpecificationAttribute(
            response.data
          );
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
  updateSpecificationAttribute(data: any): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<Brand>>(
        environment.baseUrl + 'specificationattribute/update',
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
