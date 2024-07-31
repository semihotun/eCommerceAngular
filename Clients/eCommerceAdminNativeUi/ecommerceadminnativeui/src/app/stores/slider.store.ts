import { Injectable } from '@angular/core';
import { ComponentStore } from '@ngrx/component-store';
import { Slider } from '../models/responseModel/slider';

export type SliderState = {
  slider: Slider;
};

export const sliderInitialState: SliderState = {
  slider: new Slider(),
};

@Injectable({
  providedIn: 'root',
})
export class SliderStore extends ComponentStore<SliderState> {
  readonly slider$ = this.selectSignal((x) => x.slider);

  constructor() {
    super(sliderInitialState);
  }

  readonly setSlider = this.updater((state, slider: Slider) => ({
    ...state,
    slider,
  }));
}
