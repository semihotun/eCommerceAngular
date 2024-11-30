import { Injectable, inject } from '@angular/core';
import { HttpService } from './core/http.service';
import { Destroyable } from '../shared/destroyable.service';
import { environment } from 'src/environments/environment';
import { Slider } from '../models/responseModel/Slider';
import { HomeStore } from '../stores/home.store';
import { Result } from '../models/core/result';
import { AllShowcaseDTO } from '../models/responseModel/allShowcaseDTO';
import { WebsiteInfo } from '../models/responseModel/websiteInfo';
@Injectable({
  providedIn: 'root',
})
export class HomeService extends Destroyable {
  http = inject(HttpService);
  homeStore = inject(HomeStore);

  constructor() {
    super();
  }
  getWebsiteInfo(): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.get<Result<WebsiteInfo>>(
        environment.baseUrl + 'websiteInfo/get',
        {},
        this.onDestroy,
        (response) => {
          this.homeStore.setWebsiteInfo(response.data);
          resolve();
        },
        (err) => {
          reject();
        }
      );
    });
  }
  getHomeDTO(): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.get<Result<any>>(
        environment.baseUrl + 'home/gethomedto',
        {},
        this.onDestroy,
        (response) => {
          this.homeStore.setSliders(response.data.sliderList);
          this.homeStore.setShowcases(response.data.showcaseList);
          resolve();
        },
        (err) => {
          reject();
        }
      );
    });
  }
  // getShowCaseList(): Promise<void> {
  //   return new Promise((resolve, reject) => {
  //     this.http.get<Result<AllShowcaseDTO[]>>(
  //       environment.baseUrl + 'showcase/getallhome',
  //       {},
  //       this.onDestroy,
  //       (response) => {
  //         this.homeStore.setShowcases(response.data);
  //         resolve();
  //       },
  //       (err) => {
  //         reject();
  //       }
  //     );
  //   });
  // }
}
