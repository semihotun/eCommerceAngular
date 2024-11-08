import { Injectable, inject } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpService } from './core/http.service';
import { Brand } from '../models/responseModel/brand';
import { Result } from '../models/core/result';
import { Destroyable } from '../shared/destroyable.service';
import { BrandStore } from '../stores/brand.store';
import { ToastService } from './core/toast.service';
import { AccessToken } from '../models/responseModel/accesstoken';

@Injectable({
  providedIn: 'root',
})
export class AuthService extends Destroyable {
  constructor() {
    super();
  }
  http = inject(HttpService);
  toast = inject(ToastService);

  login(data: any): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<AccessToken>>(
        environment.baseUrl + 'adminuser/adminuserlogin',
        data,
        this.onDestroy,
        (response) => {
          this.toast.presentSuccessToast();
          localStorage.setItem('token', response.data.token);
          resolve();
        },
        (err) => {
          this.toast.presentDangerToast();
          reject();
        }
      );
    });
  }
  register(data: any): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<AccessToken>>(
        environment.baseUrl + 'adminuser/adminuserregister',
        data,
        this.onDestroy,
        (response) => {
          console.log(response);
          this.toast.presentSuccessToast();
          localStorage.setItem('token', response.data.token);
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
