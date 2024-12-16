import { Injectable } from '@angular/core';
import { ComponentStore } from '@ngrx/component-store';
import { PagedList } from '../models/core/grid';
import { GetHomeShowcaseDetailDTO } from '../models/responseModel/getHomeShowcaseDetailDTO';

export type ShowcaseState = {
  getHomeShowcaseDetailDTO: PagedList<GetHomeShowcaseDetailDTO>;
};

export const productFavoriteInitialState: ShowcaseState = {
  getHomeShowcaseDetailDTO: new PagedList<GetHomeShowcaseDetailDTO>(),
};

@Injectable({
  providedIn: 'root',
})
export class ShowcaseStore extends ComponentStore<ShowcaseState> {
  readonly getHomeShowcaseDetailDTO$ = this.selectSignal(
    (x) => x.getHomeShowcaseDetailDTO
  );

  constructor() {
    super(productFavoriteInitialState);
  }
  readonly addShowcaseList = this.updater(
    (state, getHomeShowcaseDetailDTO: PagedList<GetHomeShowcaseDetailDTO>) => ({
      ...state,
      getHomeShowcaseDetailDTO: {
        ...getHomeShowcaseDetailDTO,
        data: [
          ...state.getHomeShowcaseDetailDTO.data,
          ...getHomeShowcaseDetailDTO.data,
        ],
      },
    })
  );
  readonly setShowcaseList = this.updater(
    (state, getHomeShowcaseDetailDTO: PagedList<GetHomeShowcaseDetailDTO>) => {
      return {
        ...state,
        getHomeShowcaseDetailDTO,
      };
    }
  );
}
