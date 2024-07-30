import { Injectable } from '@angular/core';
import { ComponentStore } from '@ngrx/component-store';
import { SpecificationAttribute } from '../models/responseModel/specificationattribute';
import { PagedList } from '../models/core/grid';

export type SpecificationAttributeState = {
  specificationAttribute: SpecificationAttribute;
  specificationAttributeOptionGrid: PagedList<any>;
};

export const specificationAttributeInitialState: SpecificationAttributeState = {
  specificationAttribute: new SpecificationAttribute(),
  specificationAttributeOptionGrid: new PagedList<any>(),
};

@Injectable({
  providedIn: 'root',
})
export class SpecificationAttributeStore extends ComponentStore<SpecificationAttributeState> {
  readonly specificationAttribute$ = this.selectSignal(
    (x) => x.specificationAttribute
  );
  readonly specificationAttributeOptionGridObservable$ = this.select(
    (x) => x.specificationAttributeOptionGrid
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
  readonly setSpecificationAttributeOptionGrid = this.updater(
    (state, specificationAttributeOptionGrid: PagedList<any>) => ({
      ...state,
      specificationAttributeOptionGrid,
    })
  );
}
