import { inject, Injectable } from '@angular/core';
import { Destroyable } from 'src/app/shared/destroyable.service';
import { Result } from 'src/app/models/core/result';
import { environment } from 'src/environments/environment';
import { HttpService } from './core/http.service';
import { ToastService } from './core/toast.service';
import { PagedList } from '../models/core/grid';
import { ShowcaseStore } from '../stores/shocase.store';

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

  getHomeShowcaseDetail(data: any): Promise<void> {
    return new Promise((resolve, reject) => {
      const url = environment.baseUrl + 'home/gethomeshowcasedetailquery';
      this.http.post<Result<PagedList<any>>>(
        url,
        data,
        this.onDestroy,
        (response) => {
          this.showcaseStore.addShowcaseList(response.data);
          resolve();
        },
        (err) => {
          console.log(err);
          reject();
        }
      );
    });
  }
}
