import { Injectable, inject } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpService } from './core/http.service';
import { CustomerUserAddress } from '../models/responseModel/customerUserAddress';
import { Result } from '../models/core/result';
import { Destroyable } from '../shared/destroyable.service';
import { ToastService } from './core/toast.service';
import { CustomerUserAddressStore } from '../stores/customer-user-address.store';
import { GetAllCustomerUserAddressDTO } from '../models/responseModel/getAllCustomerUserAddressDTO';

@Injectable({
  providedIn: 'root',
})
export class CustomerUserAddressService extends Destroyable {
  constructor() {
    super();
  }
  http = inject(HttpService);
  customerUserAddressStore = inject(CustomerUserAddressStore);
  toast = inject(ToastService);

  getAllCustomerUserAdressDto(): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.get<Result<GetAllCustomerUserAddressDTO[]>>(
        environment.baseUrl + 'customerUserAddress/getallcustomeruseradressdto',
        {},
        this.onDestroy,
        (response) => {
          this.customerUserAddressStore.setCustomerUserAddressDTOList(
            response.data
          );
          resolve();
        },
        (err) => {
          reject();
        }
      );
    });
  }
  getCustomerUserAddressById(id: string): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.get<Result<CustomerUserAddress>>(
        environment.baseUrl + 'customerUserAddress/getbyid',
        { id: id },
        this.onDestroy,
        (response) => {
          console.log(response.data);
          this.customerUserAddressStore.setCustomerUserAddress(response.data);
          resolve();
        },
        (err) => {
          reject();
        }
      );
    });
  }
  createcustomerUserAddress(data: any): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<CustomerUserAddress>>(
        environment.baseUrl + 'customerUserAddress/create',
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
  updateCustomerUserAddress(data: any): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<CustomerUserAddress>>(
        environment.baseUrl + 'customerUserAddress/update',
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
  deleteCustomerUserAddress(id: string): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<CustomerUserAddress>>(
        environment.baseUrl + 'customerUserAddress/delete',
        { id: id },
        this.onDestroy,
        (response) => {
          this.customerUserAddressStore.deleteCustomerUserAddressDTOListById(
            id
          );
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
