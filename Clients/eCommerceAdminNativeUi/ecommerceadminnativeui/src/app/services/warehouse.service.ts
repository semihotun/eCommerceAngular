import { Injectable, inject } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpService } from './core/http.service';
import { Warehouse } from '../models/responseModel/warehouse';
import { Result } from '../models/core/result';
import { Destroyable } from '../shared/destroyable.service';
import { ToastService } from './core/toast.service';
import { WarehouseStore } from '../stores/warehouse.store';

@Injectable({
  providedIn: 'root',
})
export class WarehouseService extends Destroyable {
  constructor() {
    super();
  }
  http = inject(HttpService);
  warehouseStore = inject(WarehouseStore);
  toast = inject(ToastService);
  getAllWarehouse(): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.get<Result<Warehouse[]>>(
        environment.baseUrl + 'warehouse/getall',
        {},
        this.onDestroy,
        (response) => {
          this.warehouseStore.setWarehouseList(response.data);
          resolve();
        },
        (err) => {
          reject();
        }
      );
    });
  }

  getWarehouseById(id: string): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.get<Result<Warehouse>>(
        environment.baseUrl + 'warehouse/getbyid',
        { id: id },
        this.onDestroy,
        (response) => {
          console.log(response.data);
          this.warehouseStore.setWarehouse(response.data);
          resolve();
        },
        (err) => {
          reject();
        }
      );
    });
  }
  createwarehouse(data: any): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<Warehouse>>(
        environment.baseUrl + 'warehouse/create',
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
  updateWarehouse(data: any): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<Warehouse>>(
        environment.baseUrl + 'warehouse/update',
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
