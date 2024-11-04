import { Injectable, inject } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpService } from '../core/http.service';
import { Result } from '../../models/core/result';
import { Destroyable } from '../../shared/destroyable.service';
import { ToastService } from '../core/toast.service';
import { SpecificationAttributeStore } from '../../stores/specificationattribute.store';
import { SpecificationAttributeOption } from '../../models/responseModel/specificationattribute';
import { GridStore } from './../../stores/grid.store';
import { GridPostData, PagedList } from './../../models/core/grid';

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
  gridStore = inject(GridStore);
  createSpecificationOptionAttribute(data: any): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<SpecificationAttributeOption>>(
        environment.baseUrl + 'specificationattributeoption/create',
        data,
        this.onDestroy,
        async (response) => {
          await this.getSpecificationOptionAttributeGrid({
            ...new GridPostData(1, 10),
            specificationAttributeId: data.specificationAttributeId,
          });
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
  getSpecificationOptionAttributeGrid(data?: any): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<PagedList<any>>>(
        environment.baseUrl + 'specificationattributeoption/getgrid',
        data,
        this.onDestroy,
        (response) => {
          this.specificationAttributeStore.setSpecificationAttributeOptionGrid(
            response.data
          );
          resolve();
        },
        (err) => {
          this.toast.presentDangerToast();
          reject();
        }
      );
    });
  }
  getAllSpecificationAttributeOptionBySpecId(specId: string): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.get<Result<SpecificationAttributeOption[]>>(
        environment.baseUrl + 'specificationattributeoption/getallbyspecid',
        { id: specId },
        this.onDestroy,
        (response) => {
          this.specificationAttributeStore.setSpecificationAttributeOptionList(
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
}
