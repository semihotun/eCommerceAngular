import { inject, Injectable } from '@angular/core';
import { HttpService } from '../core/http.service';
import { ShowcaseStore } from 'src/app/stores/showcase.store';
import { ToastService } from '../core/toast.service';
import { ShowCaseType } from 'src/app/models/responseModel/showcaseType';
import { Result } from 'src/app/models/core/result';
import { environment } from 'src/environments/environment';
import { Destroyable } from 'src/app/shared/destroyable.service';
import { ShowCase } from './../../models/responseModel/showcase';

@Injectable({
  providedIn: 'root',
})
export class ShowcaseTypeService extends Destroyable {
  constructor() {
    super();
  }
  http = inject(HttpService);
  showcaseStore = inject(ShowcaseStore);
  toast = inject(ToastService);
  getAllShowCaseType(): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.get<Result<ShowCaseType[]>>(
        environment.baseUrl + 'showcasetype/getall',
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
