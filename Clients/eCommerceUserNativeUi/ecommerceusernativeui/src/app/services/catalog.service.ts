import { Injectable, inject } from '@angular/core';
import { HttpService } from './core/http.service';
import { Destroyable } from '../shared/destroyable.service';
import { environment } from 'src/environments/environment';
import { Result } from '../models/core/result';
import { LoaderService } from './core/loader.service';
import { CatalogStore } from '../stores/catalog.store';
import { PagedList } from '../models/core/grid';
import { GetCatalogProductDTO } from '../models/responseModel/getCatalogProductDTO';
import { GetCatalogDTO } from '../models/responseModel/getCatalogDTO';
import { GetCatalogProductDTOBySlugRequest } from './../models/requestModel/getCatalogProductDTO';
@Injectable({
  providedIn: 'root',
})
export class CatalogService extends Destroyable {
  http = inject(HttpService);
  catalogStore = inject(CatalogStore);
  loaderService = inject(LoaderService);
  constructor() {
    super();
  }
  getCatalogDTOBySlug(categorySlug: string): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<GetCatalogDTO>>(
        environment.baseUrl + 'home/getcatalogdtobyslug',
        { categorySlug: categorySlug },
        this.onDestroy,
        (response) => {
          this.catalogStore.setCatalogDto(response.data);
          resolve();
        },
        (err) => {
          reject();
        }
      );
    });
  }
  getCatalogProductDtoBySlug(
    data: GetCatalogProductDTOBySlugRequest
  ): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<PagedList<GetCatalogProductDTO>>>(
        environment.baseUrl + 'home/getcatalogProductbyslug',
        data,
        this.onDestroy,
        (response) => {
          this.catalogStore.addCatalogProductDto(response.data);
          resolve();
        },
        (err) => {
          reject();
        }
      );
    });
  }
}
