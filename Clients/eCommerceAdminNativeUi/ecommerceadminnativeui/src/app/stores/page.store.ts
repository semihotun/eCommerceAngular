import { Injectable } from '@angular/core';
import { ComponentStore } from '@ngrx/component-store';
import { Page } from '../models/responseModel/page';

export type PageState = {
  page: Page;
};

export const pageInitialState: PageState = {
  page: new Page(),
};

@Injectable({
  providedIn: 'root',
})
export class PageStore extends ComponentStore<PageState> {
  readonly page$ = this.selectSignal((x) => x.page);

  constructor() {
    super(pageInitialState);
  }

  readonly setPage = this.updater((state, page: Page) => ({
    ...state,
    page,
  }));
}
