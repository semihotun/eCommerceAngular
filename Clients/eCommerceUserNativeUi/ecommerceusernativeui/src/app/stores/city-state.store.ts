import { Injectable } from '@angular/core';
import { ComponentStore } from '@ngrx/component-store';
import { PagedList } from '../models/core/grid';
import { City } from '../models/responseModel/city';
import { District } from '../models/responseModel/district';

export type CityAndDistrictState = {
  cityList: PagedList<City>;
  districtList: PagedList<District>;
};

export const cityAndDistrictStateInitialState: CityAndDistrictState = {
  cityList: new PagedList<City>(),
  districtList: new PagedList<District>(),
};

@Injectable({
  providedIn: 'root',
})
export class CityAndDistrictStore extends ComponentStore<CityAndDistrictState> {
  readonly cityList$ = this.selectSignal((x) => x.cityList);
  readonly districtList$ = this.selectSignal((x) => x.districtList);

  constructor() {
    super(cityAndDistrictStateInitialState);
  }
  readonly addCityList = this.updater((state, cityList: PagedList<City>) => ({
    ...state,
    cityList: {
      ...cityList,
      data: [...state.cityList.data, ...cityList.data],
    },
  }));
  readonly setCityList = this.updater((state, cityList: PagedList<City>) => ({
    ...state,
    cityList,
  }));
  readonly addDistrictList = this.updater(
    (state, districtList: PagedList<District>) => ({
      ...state,
      districtList: {
        ...districtList,
        data: [...state.districtList.data, ...districtList.data],
      },
    })
  );
  readonly setDistrictList = this.updater(
    (state, districtList: PagedList<District>) => ({
      ...state,
      districtList,
    })
  );
}
