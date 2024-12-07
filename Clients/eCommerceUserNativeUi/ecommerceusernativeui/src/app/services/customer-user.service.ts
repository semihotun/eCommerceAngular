import { inject, Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Result } from '../models/core/result';
import { HttpService } from './core/http.service';
import { Destroyable } from '../shared/destroyable.service';
import { Router } from '@angular/router';
import { ToastService } from './core/toast.service';
import { AccessToken } from '../models/responseModel/accesstoken';
import { CustomerUserDTO } from '../models/responseModel/customerUserDTO';
import { CustomerUserStore } from '../stores/customer-user.store';

@Injectable({
  providedIn: 'root',
})
export class CustomerUserService extends Destroyable {
  router = inject(Router);
  constructor() {
    super();
  }
  private http = inject(HttpService);
  toast = inject(ToastService);
  customerUserStore = inject(CustomerUserStore);
  login(data: any): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<AccessToken>>(
        environment.baseUrl + 'customeruser/customeruserlogin',
        data,
        this.onDestroy,
        (response) => {
          this.toast.presentSuccessToast();
          localStorage.setItem('token', response.data.token);
          resolve();
        },
        (err) => {
          this.toast.presentDangerToast(err.error.message);
          reject();
        }
      );
    });
  }
  register(data: any): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<AccessToken>>(
        environment.baseUrl + 'customeruser/customeruserregister',
        data,
        this.onDestroy,
        (response) => {
          this.toast.presentSuccessToast();
          localStorage.setItem('token', response.data.token);
          resolve();
        },
        (err) => {
          this.toast.presentDangerToast(err.error.message);
          reject();
        }
      );
    });
  }
  customerUserActivationConfirmation(data: any): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<any>>(
        environment.baseUrl + 'customeruser/customeruseractivationconfirmation',
        data,
        this.onDestroy,
        (response) => {
          localStorage.setItem('token', response.data.token);
          this.toast.presentSuccessToast(response.message);
          resolve();
        },
        (err) => {
          this.toast.presentDangerToast(err.error.message);
          reject();
        }
      );
    });
  }
  getCustomerUserDto(): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<CustomerUserDTO>>(
        environment.baseUrl + 'customeruser/getcustomeruserbyid',
        {},
        this.onDestroy,
        (response) => {
          this.customerUserStore.setCustomerUserDto(response.data);
          resolve();
        },
        (err) => {
          reject();
        }
      );
    });
  }
  updateCustomerUser(data: any): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<CustomerUserDTO>>(
        environment.baseUrl + 'customeruser/updatecustomeruser',
        data,
        this.onDestroy,
        (response) => {
          this.toast.presentSuccessToast();
          resolve();
        },
        (err) => {
          this.toast.presentDangerToast(err.error.message);
          reject();
        }
      );
    });
  }
  verifyAccount(): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<CustomerUserDTO>>(
        environment.baseUrl + 'customeruser/verifymailsend',
        {},
        this.onDestroy,
        (response) => {
          this.toast.presentSuccessToast(response.message);
          resolve();
        },
        (err) => {
          this.toast.presentDangerToast(err.error.message);
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
    this.router.navigate(['']);
  }
}
