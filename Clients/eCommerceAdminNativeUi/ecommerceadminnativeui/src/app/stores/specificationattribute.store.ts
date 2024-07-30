import { Injectable } from '@angular/core';
import { ComponentStore } from '@ngrx/component-store';
import { SpecificationAttribute } from '../models/responseModel/specificationattribute';

export type SpecificationAttributeState = {
  specificationAttribute: SpecificationAttribute;
};

export const specificationAttributeInitialState: SpecificationAttributeState = {
  specificationAttribute: new SpecificationAttribute(),
};

@Injectable({
  providedIn: 'root',
})
export class SpecificationAttributeStore extends ComponentStore<SpecificationAttributeState> {
  readonly specificationAttribute$ = this.selectSignal(
    (x) => x.specificationAttribute
  );

  constructor() {
    super(specificationAttributeInitialState);
  }

  readonly setSpecificationAttribute = this.updater(
    (state, specificationAttribute: SpecificationAttribute) => ({
      ...state,
      specificationAttribute,
    })
  );
}
