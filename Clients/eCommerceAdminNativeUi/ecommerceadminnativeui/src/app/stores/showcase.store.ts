import { Injectable } from '@angular/core';
import { ComponentStore } from '@ngrx/component-store';
import { ShowCaseType } from '../models/responseModel/showcaseType';
import { ShowCase } from '../models/responseModel/showcase';
import { PagedList } from '../models/core/grid';
import { ShowCaseProduct } from '../models/responseModel/showcaseProduct';

export type ShowCaseState = {
  showcaseTypeList: ShowCaseType[];
  showcase: ShowCase;
  showcaseProductGrid: PagedList<ShowCaseProduct>;
};

export const showCaseInitialState: ShowCaseState = {
  showcaseTypeList: [],
  showcase: new ShowCase(),
  showcaseProductGrid: new PagedList<ShowCaseProduct>(),
};

@Injectable({
  providedIn: 'root',
})
export class ShowcaseStore extends ComponentStore<ShowCaseState> {
  readonly showcaseTypeList$ = this.selectSignal((x) => x.showcaseTypeList);
  readonly showcase$ = this.selectSignal((x) => x.showcase);
  readonly showcaseProductGridObservable$ = this.select(
    (x) => x.showcaseProductGrid
  );
  constructor() {
    super(showCaseInitialState);
  }
  readonly setShowcaseTypeList = this.updater(
    (state, showcaseTypeList: ShowCaseType[]) => ({
      ...state,
      showcaseTypeList,
    })
  );
  readonly setShowcase = this.updater((state, showcase: ShowCase) => ({
    ...state,
    showcase,
  }));
  readonly setShowcaseProductGrid = this.updater(
    (state, showcaseProductGrid: PagedList<ShowCaseProduct>) => ({
      ...state,
      showcaseProductGrid,
    })
  );
}
