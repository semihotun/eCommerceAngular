import { inject, Injectable } from '@angular/core';
import { Destroyable } from 'src/app/shared/destroyable.service';
import { HttpService } from './core/http.service';
import { ToastService } from './core/toast.service';
import { Result } from '../models/core/result';
import { environment } from 'src/environments/environment';
import { PagedList } from '../models/core/grid';
import { City } from '../models/responseModel/city';
import { District } from '../models/responseModel/district';
import { CityAndDistrictStore } from '../stores/city-state.store';
import { LoaderService } from './core/loader.service';
@Injectable({
  providedIn: 'root',
})
export class CityDistrictService extends Destroyable {
  constructor() {
    super();
  }
  http = inject(HttpService);
  toast = inject(ToastService);
  loaderService = inject(LoaderService);
  cityAndDistrictStore = inject(CityAndDistrictStore);

  getAllCity(
    pageIndex: number,
    pageSize: number,
    city: string,
    replace: boolean = false
  ): Promise<void> {
    return new Promise((resolve, reject) => {
      const url = environment.baseUrl + 'cityanddistrict/getallcity';
      this.loaderService.addExcludedUrl(url);

      this.http.post<Result<PagedList<City>>>(
        url,
        { PageIndex: pageIndex, PageSize: pageSize, City: city },
        this.onDestroy,
        (response) => {
          if (replace) {
            this.cityAndDistrictStore.setCityList(response.data);
          } else {
            this.cityAndDistrictStore.addCityList(response.data);
          }
          resolve();
        },
        (err) => {
          this.toast.presentDangerToast();
          reject();
        }
      );
    });
  }
  getAllDistrictByCityId(
    pageIndex: number,
    pageSize: number,
    district: string,
    replace: boolean = false,
    cityId: string
  ): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<PagedList<District>>>(
        environment.baseUrl + 'cityanddistrict/getalldistrictbycityid',
        {
          CityId: cityId,
          PageIndex: pageIndex,
          PageSize: pageSize,
          District: district,
        },
        this.onDestroy,
        (response) => {
          if (replace) {
            this.cityAndDistrictStore.setDistrictList(response.data);
          } else {
            this.cityAndDistrictStore.addDistrictList(response.data);
          }
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
