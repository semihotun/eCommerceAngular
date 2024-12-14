import { Injectable } from '@angular/core';
import { ComponentStore } from '@ngrx/component-store';
import { CustomerUserAddress } from '../models/responseModel/customerUserAddress';
import { GetAllCustomerUserAddressDTO } from './../models/responseModel/getAllCustomerUserAddressDTO';

export type CustomerUserAddressState = {
  customerUserAddress: CustomerUserAddress;
  customerUserAddressDTOList: GetAllCustomerUserAddressDTO[];
};

export const customerUserAddressInitialState: CustomerUserAddressState = {
  customerUserAddress: new CustomerUserAddress(),
  customerUserAddressDTOList: [],
};

@Injectable({
  providedIn: 'root',
})
export class CustomerUserAddressStore extends ComponentStore<CustomerUserAddressState> {
  readonly customerUserAddress$ = this.selectSignal(
    (x) => x.customerUserAddress
  );
  readonly customerUserAddressDTOList$ = this.selectSignal(
    (x) => x.customerUserAddressDTOList
  );
  constructor() {
    super(customerUserAddressInitialState);
  }

  readonly setCustomerUserAddress = this.updater(
    (state, customerUserAddress: CustomerUserAddress) => ({
      ...state,
      customerUserAddress,
    })
  );
  readonly deleteCustomerUserAddressDTOListById = this.updater(
    (state, id: string) => {
      const updatedList = state.customerUserAddressDTOList.filter(
        (x) => x.id !== id
      );
      return {
        ...state,
        customerUserAddressDTOList: updatedList,
      };
    }
  );
  readonly setCustomerUserAddressDTOList = this.updater(
    (state, customerUserAddressDTOList: GetAllCustomerUserAddressDTO[]) => ({
      ...state,
      customerUserAddressDTOList,
    })
  );
}
