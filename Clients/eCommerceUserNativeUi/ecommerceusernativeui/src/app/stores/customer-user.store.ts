import { Injectable } from '@angular/core';
import { ComponentStore } from '@ngrx/component-store';
import { CustomerUserDTO } from '../models/responseModel/customerUserDTO';

export type CustomerUserState = {
  customerUserDto: CustomerUserDTO;
};

export const customerUserInitialState: CustomerUserState = {
  customerUserDto: new CustomerUserDTO(),
};

@Injectable({
  providedIn: 'root',
})
export class CustomerUserStore extends ComponentStore<CustomerUserState> {
  readonly customerUserDto$ = this.selectSignal((x) => x.customerUserDto);

  constructor() {
    super(customerUserInitialState);
  }

  readonly setCustomerUserDto = this.updater(
    (state, customerUserDto: CustomerUserDTO) => ({
      ...state,
      customerUserDto,
    })
  );
}
