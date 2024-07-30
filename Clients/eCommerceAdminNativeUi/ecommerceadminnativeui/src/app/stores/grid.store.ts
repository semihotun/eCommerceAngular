import { Injectable } from '@angular/core';
import { ComponentStore } from '@ngrx/component-store';
import { GridSettingsDTO, PagedList } from '../models/core/grid';

export type GridState = {
  data: PagedList<any[]>;
  gridSettings: GridSettingsDTO;
};

export const gridInitialState: GridState = {
  data: new PagedList<any[]>(),
  gridSettings: new GridSettingsDTO(),
};

@Injectable({
  providedIn: 'root',
})
export class GridStore extends ComponentStore<GridState> {
  readonly dataSignal$ = this.selectSignal((x) => x.data);
  readonly gridSettings$ = this.selectSignal((x) => x.gridSettings);

  constructor() {
    super(gridInitialState);
  }

  readonly setData = this.updater((state, data: PagedList<any[]>) => ({
    ...state,
    data,
  }));
  readonly setGridSettings = this.updater(
    (state, gridSettings: GridSettingsDTO) => ({
      ...state,
      gridSettings,
    })
  );
  readonly setGridSettingsPropertyInfo = this.updater(
    (state, propertyInfo: any) => ({
      ...state,
      gridSettings: {
        ...state.gridSettings,
        propertyInfo: propertyInfo,
      },
    })
  );
}
