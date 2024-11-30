import { Injectable } from '@angular/core';
import { Slider } from '../models/responseModel/Slider';
import { ComponentStore } from '@ngrx/component-store';
import { AllShowcaseDTO } from '../models/responseModel/allShowcaseDTO';
import { WebsiteInfo } from '../models/responseModel/websiteInfo';

export type HomeState = {
  sliders: Slider[];
  showcases: AllShowcaseDTO[];
  websiteInfo: WebsiteInfo;
};

export const homeInitialState: HomeState = {
  sliders: [],
  showcases: [],
  websiteInfo: new WebsiteInfo(),
};

@Injectable({
  providedIn: 'root',
})
export class HomeStore extends ComponentStore<HomeState> {
  readonly sliders$ = this.selectSignal((x) => x.sliders);
  readonly showcases$ = this.selectSignal((x) => x.showcases);
  readonly websiteInfo$ = this.selectSignal((x) => x.websiteInfo);
  constructor() {
    super(homeInitialState);
  }

  readonly setSliders = this.updater((state, sliders: Slider[]) => ({
    ...state,
    sliders,
  }));

  readonly setShowcases = this.updater(
    (state, showcases: AllShowcaseDTO[]) => ({
      ...state,
      showcases,
    })
  );
  readonly setWebsiteInfo = this.updater((state, websiteInfo: WebsiteInfo) => ({
    ...state,
    websiteInfo,
  }));
}
