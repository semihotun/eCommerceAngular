import { inject, Injectable } from '@angular/core';
import { HttpService } from '../core/http.service';
import { ShowcaseStore } from 'src/app/stores/showcase.store';
import { ToastService } from '../core/toast.service';
import { ShowCaseType } from 'src/app/models/responseModel/showcaseType';
import { Result } from 'src/app/models/core/result';
import { environment } from 'src/environments/environment';
import { Destroyable } from 'src/app/shared/destroyable.service';
import { ShowCase } from 'src/app/models/responseModel/showcase';

@Injectable({
  providedIn: 'root',
})
export class ShowcaseService extends Destroyable {
  constructor() {
    super();
  }
  http = inject(HttpService);
  showcaseStore = inject(ShowcaseStore);
  toast = inject(ToastService);

  getShowcaseById(id: string): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.get<Result<ShowCase>>(
        environment.baseUrl + 'showcase/getbyid',
        { id: id },
        this.onDestroy,
        (response) => {
          console.log(response.data);
          this.showcaseStore.setShowcase(response.data);
          resolve();
        },
        (err) => {
          reject();
        }
      );
    });
  }
  createShowcase(data: any): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<ShowCase>>(
        environment.baseUrl + 'showcase/create',
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
  updateShowcase(data: any): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<ShowCase>>(
        environment.baseUrl + 'showcase/update',
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

  getAllShowCaseType(): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.get<Result<ShowCaseType[]>>(
        environment.baseUrl + 'showcase/getall',
        {},
        this.onDestroy,
        (response) => {
          this.showcaseStore.setShowcaseTypeList(response.data);
          resolve();
        },
        (err) => {
          reject();
        }
      );
    });
  }
}
