import { Injectable, inject } from '@angular/core';
import { HttpService } from './http.service';
import { Destroyable } from '../shared/destroyable.service';
import { environment } from 'src/environments/environment';
import { Slider } from '../models/responseModel/Slider';
import { HomeStore } from '../stores/home.store';
import { Showcase } from '../models/responseModel/Showcase';
import { HttpHeaders } from '@angular/common/http';
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
  getAllSlider(): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.get<Result<Slider[]>>(
        environment.baseUrl + 'slider/getall',
        {},
        this.onDestroy,
        (response) => {
          this.homeStore.setSliders(response.data);
          resolve();
        },
        (err) => {
          reject();
        }
      );
    });
  }

  getShowCaseList(): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.get<Result<AllShowcaseDTO[]>>(
        environment.baseUrl + 'showcase/getallhome',
        {},
        this.onDestroy,
        (response) => {
          this.homeStore.setShowcases(response.data);
          resolve();
        },
        (err) => {
          reject();
        }
      );
    });
  }
}
