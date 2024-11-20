import { Injectable } from '@angular/core';
import { ComponentStore } from '@ngrx/component-store';
import { WebsiteInfo } from '../models/responseModel/websiteInfo';

export type WebsiteInfoState = {
  websiteInfo: WebsiteInfo;
};

export const websiteInfoInitialState: WebsiteInfoState = {
  websiteInfo: new WebsiteInfo(),
};

@Injectable({
  providedIn: 'root',
})
export class WebsiteInfoStore extends ComponentStore<WebsiteInfoState> {
  readonly websiteInfo$ = this.selectSignal((x) => x.websiteInfo);
  constructor() {
    super(websiteInfoInitialState);
  }

  readonly setWebsiteInfo = this.updater((state, websiteInfo: WebsiteInfo) => ({
    ...state,
    websiteInfo,
  }));
}
