import { Injectable } from '@angular/core';
import { ComponentStore } from '@ngrx/component-store';
import { Brand } from '../models/responseModel/brand';
import { Warehouse } from '../models/responseModel/warehouse';

export type WarehouseState = {
  warehouse: Warehouse;
  warehouseList: Warehouse[];
};

export const warehouseInitialState: WarehouseState = {
  warehouse: new Warehouse(),
  warehouseList: [],
};

@Injectable({
  providedIn: 'root',
})
export class WarehouseStore extends ComponentStore<WarehouseState> {
  readonly warehouse$ = this.selectSignal((x) => x.warehouse);
  readonly warehouseList$ = this.selectSignal((x) => x.warehouseList);
  constructor() {
    super(warehouseInitialState);
  }

  readonly setWarehouse = this.updater((state, warehouse: Warehouse) => ({
    ...state,
    warehouse,
  }));

  readonly setWarehouseList = this.updater(
    (state, warehouseList: Warehouse[]) => ({
      ...state,
      warehouseList,
    })
  );
}
