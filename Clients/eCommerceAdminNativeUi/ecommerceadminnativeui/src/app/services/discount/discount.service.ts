import { Injectable, inject } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpService } from '../core/http.service';
import { Result } from '../../models/core/result';
import { Destroyable } from '../../shared/destroyable.service';
import { ToastService } from '../core/toast.service';
import { DiscountStore } from 'src/app/stores/discount.store';
import { Discount } from 'src/app/models/responseModel/discount';

@Injectable({
  providedIn: 'root',
})
export class DiscountService extends Destroyable {
  constructor() {
    super();
  }
  http = inject(HttpService);
  discountStore = inject(DiscountStore);
  toast = inject(ToastService);
  //   getAllDiscount(): Promise<void> {
  //     return new Promise((resolve, reject) => {
  //       this.http.get<Result<Discount[]>>(
  //         environment.baseUrl + 'discount/getall',
  //         {},
  //         this.onDestroy,
  //         (response) => {
  //           this.discountStore.setDiscountList(response.data);
  //           resolve();
  //         },
  //         (err) => {
  //           reject();
  //         }
  //       );
  //     });
  //   }

  getDiscountById(id: string): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.get<Result<Discount>>(
        environment.baseUrl + 'discount/getbyid',
        { id: id },
        this.onDestroy,
        (response) => {
          console.log(response.data);
          this.discountStore.setDiscount(response.data);
          resolve();
        },
        (err) => {
          reject();
        }
      );
    });
  }
  creatediscount(data: any): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<Discount>>(
        environment.baseUrl + 'discount/create',
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
  // updateDiscount(data: any): Promise<void> {
  //   return new Promise((resolve, reject) => {
  //     this.http.post<Result<Discount>>(
  //       environment.baseUrl + 'discount/update',
  //       data,
  //       this.onDestroy,
  //       (response) => {
  //         this.toast.presentSuccessToast();
  //         resolve();
  //       },
  //       (err) => {
  //         this.toast.presentDangerToast();
  //         reject();
  //       }
  //     );
  //   });
  // }
}
