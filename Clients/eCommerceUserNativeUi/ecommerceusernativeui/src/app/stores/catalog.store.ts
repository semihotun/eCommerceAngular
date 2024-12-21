import { Injectable } from '@angular/core';
import { ComponentStore } from '@ngrx/component-store';
import { WebsiteInfo } from '../models/responseModel/websiteInfo';
import { GetCatalogDTO } from '../models/responseModel/getCatalogDTO';
import { PagedList } from '../models/core/grid';
import { GetCatalogProductDTO } from '../models/responseModel/getCatalogProductDTO';
import { CatalogFilter } from '../models/requestModel/getCatalogProductDTO';
import { Subject } from 'rxjs';

export type CatalogState = {
  catalogDto: GetCatalogDTO;
  catalogProductDto: PagedList<GetCatalogProductDTO>;
  filterList: CatalogFilter[];
  minPrice: number;
  maxPrice: number;
  removedOptionId: string;
};

export const catalogInitialState: CatalogState = {
  catalogDto: new GetCatalogDTO(),
  catalogProductDto: new PagedList<GetCatalogProductDTO>(),
  filterList: [],
  minPrice: 0,
  maxPrice: 0,
  removedOptionId: '',
};

@Injectable({
  providedIn: 'root',
})
export class CatalogStore extends ComponentStore<CatalogState> {
  readonly catalogDto$ = this.selectSignal((x) => x.catalogDto);
  readonly catalogProductDto$ = this.selectSignal((x) => x.catalogProductDto);
  readonly removedOptionId$ = this.select((x) => x.removedOptionId);
  readonly filterList$ = this.selectSignal((x) => x.filterList);
  readonly minPrice$ = this.selectSignal((x) => x.minPrice);
  readonly maxPrice$ = this.selectSignal((x) => x.maxPrice);
  constructor() {
    super(catalogInitialState);
  }
  readonly setCatalogDto = this.updater((state, catalogDto: GetCatalogDTO) => ({
    ...state,
    catalogDto,
  }));
  readonly setCatalogProductDto = this.updater(
    (state, catalogProductDto: PagedList<GetCatalogProductDTO>) => ({
      ...state,
      catalogProductDto,
    })
  );
  readonly setFilterList = this.updater(
    (state, filterList: CatalogFilter[]) => ({
      ...state,
      filterList,
    })
  );
  readonly setMinPrice = this.updater((state, minPrice: number) => ({
    ...state,
    minPrice,
  }));
  readonly setRemovedOptionId = this.updater(
    (state, removedOptionId: string) => ({
      ...state,
      removedOptionId,
    })
  );
  readonly setMaxPrice = this.updater((state, maxPrice: number) => ({
    ...state,
    maxPrice,
  }));
  readonly addCatalogProductDto = this.updater(
    (state, catalogProductDto: PagedList<GetCatalogProductDTO>) => ({
      ...state,
      catalogProductDto: {
        ...catalogProductDto,
        data: [...state.catalogProductDto.data, ...catalogProductDto.data],
      },
    })
  );
  readonly resetState = this.updater(() => catalogInitialState);
}
