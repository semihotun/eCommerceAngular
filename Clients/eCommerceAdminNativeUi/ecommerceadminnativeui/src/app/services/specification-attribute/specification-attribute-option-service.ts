import { Injectable, inject } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpService } from '../core/http.service';
import { Result } from '../../models/core/result';
import { Destroyable } from '../../shared/destroyable.service';
import { ToastService } from '../core/toast.service';
import { SpecificationAttributeStore } from '../../stores/specificationattribute.store';
import { SpecificationAttributeOption } from '../../models/responseModel/specificationattribute';

@Injectable({
  providedIn: 'root',
})
export class SpecificationAttributeOptionService extends Destroyable {
  constructor() {
    super();
  }
  http = inject(HttpService);
  specificationAttributeStore = inject(SpecificationAttributeStore);
  toast = inject(ToastService);
  createSpecificationAttribute(data: any): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<SpecificationAttributeOption>>(
        environment.baseUrl + 'specificationAttribute/create',
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
  updateSpecificationAttribute(data: any): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<SpecificationAttributeOption>>(
        environment.baseUrl + 'specificationAttribute/update',
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
