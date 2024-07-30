import { inject, Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Result } from '../../models/core/result';
import { HttpService } from './http.service';
import { Destroyable } from '../../shared/destroyable.service';
import {
  GridPostData,
  GridSettingsDTO,
  PagedList,
} from '../../models/core/grid';
import { GridStore } from '../../stores/grid.store';

@Injectable({
  providedIn: 'root',
})
export class GridService extends Destroyable {
  constructor() {
    super();
  }
  http = inject(HttpService);
  gridStore = inject(GridStore);
  deleteUrl(url: string, id: number): Promise<void> {
    return new Promise((resolve, reject) => {
      const path = environment.baseUrl + url;
      let data = { id: id };
      this.http.post<Result<any>>(
        path,
        data,
        this.onDestroy,
        (response) => {
          resolve();
        },
        (err) => {
          reject();
        }
      );
    });
  }
  getAllData(url: string, data: GridPostData): Promise<void> {
    return new Promise((resolve, reject) => {
      const path = environment.baseUrl + url;
      this.http.post<Result<PagedList<any>>>(
        path,
        data,
        this.onDestroy,
        (response) => {
          this.gridStore.setData(response.data);
          resolve();
        },
        (err) => {
          reject();
        }
      );
    });
  }
  getSettingsGrid(url: string): Promise<void> {
    return new Promise((resolve, reject) => {
      const path =
        environment.baseUrl + 'gridsetting/getgridsettingbypath?path=' + url;
      this.http.get<Result<GridSettingsDTO>>(
        path,
        {},
        this.onDestroy,
        (response) => {
          this.gridStore.setGridSettings(response.data);
          resolve();
        },
        (err) => {
          reject();
        }
      );
    });
  }
  updateGridSettings() {
    const path = environment.baseUrl + 'gridsetting/updategridsetting';
    let data = this.gridStore.gridSettings$();
    this.http.post<Result<GridSettingsDTO>>(
      path,
      data,
      this.onDestroy,
      (response) => {},
      (err) => {}
    );
  }
}
