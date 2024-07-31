import { Injectable, inject } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpService } from './core/http.service';
import { Slider } from '../models/responseModel/slider';
import { Result } from '../models/core/result';
import { ToastService } from './core/toast.service';
import { Destroyable } from '../shared/destroyable.service';
import { SliderStore } from '../stores/slider.store';
import { formData } from '../helpers/formData';

@Injectable({
  providedIn: 'root',
})
export class SliderService extends Destroyable {
  constructor() {
    super();
  }
  http = inject(HttpService);
  sliderStore = inject(SliderStore);
  toast = inject(ToastService);
  getSliderById(id: string): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.get<Result<Slider>>(
        environment.baseUrl + 'slider/getbyid',
        { id: id },
        this.onDestroy,
        (response) => {
          this.sliderStore.setSlider(response.data);
          resolve();
        },
        (err) => {
          reject();
        }
      );
    });
  }
  createSlider(data: any): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<Slider>>(
        environment.baseUrl + 'slider/create',
        data,
        this.onDestroy,
        (response) => {
          this.toast.presentSuccessToast();
          resolve();
        },
        (err) => {
          this.toast.presentDangerToast();
          reject();
        }
      );
    });
  }
  updateSlider(data: any): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<Slider>>(
        environment.baseUrl + 'slider/update',
        data,
        this.onDestroy,
        (response) => {
          this.toast.presentSuccessToast();
          resolve();
        },
        (err) => {
          this.toast.presentDangerToast();
          reject();
        }
      );
    });
  }
}
