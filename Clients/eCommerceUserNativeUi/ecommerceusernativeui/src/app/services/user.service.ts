import { inject, Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Result } from '../models/core/result';
import { HttpService } from './core/http.service';
import { Destroyable } from '../shared/destroyable.service';
import { Router } from '@angular/router';
import { ToastService } from './core/toast.service';

@Injectable({
  providedIn: 'root',
})
export class UserService extends Destroyable {
  router = inject(Router);
  constructor() {
    super();
  }
  private http = inject(HttpService);
  toast = inject(ToastService);

  login(data: any): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<any>>(
        environment.baseUrl + 'customeruser/customeruserlogin',
        data,
        this.onDestroy,
        (response) => {
          this.toast.presentSuccessToast();
          localStorage.setItem('token', response.data.token);
          resolve();
        },
        (err) => {
          this.toast.presentDangerToast(err.error.Message);
          reject();
        }
      );
    });
  }
  register(data: any): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<any>>(
        environment.baseUrl + 'customeruser/customeruserregister',
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
  isLogin(): boolean {
    return localStorage.getItem('token') == null ? false : true;
  }
  logOut() {
    localStorage.removeItem('token');
    localStorage.removeItem('claims');
    this.router.navigate(['']);
  }
}
