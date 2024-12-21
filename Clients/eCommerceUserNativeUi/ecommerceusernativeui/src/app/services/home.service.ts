import { Injectable, inject } from '@angular/core';
import { HttpService } from './core/http.service';
import { Destroyable } from '../shared/destroyable.service';
import { environment } from 'src/environments/environment';
import { HomeStore } from '../stores/home.store';
import { Result } from '../models/core/result';
import { WebsiteInfo } from '../models/responseModel/websiteInfo';
import { CategoryTreeDTO } from '../models/responseModel/CategoryTreeDTO';
import { ProductSearch } from '../models/responseModel/productSearch';
import { Subject } from 'rxjs';
import { LoaderService } from './core/loader.service';
@Injectable({
  providedIn: 'root',
})
export class HomeService extends Destroyable {
  http = inject(HttpService);
  homeStore = inject(HomeStore);
  loaderService = inject(LoaderService);
  constructor() {
    super();
  }
  getCatalogDTO(): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<WebsiteInfo>>(
        environment.baseUrl + 'websiteInfo/getcatalogdto',
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
  getCatalogProductDTO(): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.get<Result<WebsiteInfo>>(
        environment.baseUrl + 'websiteInfo/getcatalogProduct',
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
  private getHomeProductSearchCancelRequest$ = new Subject<void>();
  getHomeProductSearch(productName: string): Promise<void> {
    const url = environment.baseUrl + 'home/gethomeproductsearchquery';
    this.loaderService.addExcludedUrl(url);
    return new Promise((resolve, reject) => {
      this.getHomeProductSearchCancelRequest$.next();
      this.http.post<Result<ProductSearch[]>>(
        url,
        { productName: productName },
        this.getHomeProductSearchCancelRequest$,
        (response) => {
          this.homeStore.setHomeSearchList(response.data);
          resolve();
        },
        (err) => {
          reject();
        }
      );
    });
  }
  getCategoryTree(): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.get<Result<CategoryTreeDTO[]>>(
        environment.baseUrl + 'category/getall',
        {},
        this.onDestroy,
        (response) => {
          this.homeStore.setCategoryTreeDto(response.data);
          resolve();
        },
        (err) => {
          reject();
        }
      );
    });
  }
}
