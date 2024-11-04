import { Injectable } from '@angular/core';
import { ComponentStore } from '@ngrx/component-store';
import {
  SpecificationAttribute,
  SpecificationAttributeOption,
} from '../models/responseModel/specificationattribute';
import { PagedList } from '../models/core/grid';

export type SpecificationAttributeState = {
  specificationAttribute: SpecificationAttribute;
  specificationAttributeList: SpecificationAttribute[];
  specificationAttributeOptionList: SpecificationAttributeOption[];
  specificationAttributeOptionGrid: PagedList<any>;
};

export const specificationAttributeInitialState: SpecificationAttributeState = {
  specificationAttribute: new SpecificationAttribute(),
  specificationAttributeOptionGrid: new PagedList<any>(),
  specificationAttributeList: [],
  specificationAttributeOptionList: [],
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
  specificationAttributeList = this.selectSignal(
    (x) => x.specificationAttributeList
  );
  specificationAttributeOptionList = this.selectSignal(
    (x) => x.specificationAttributeOptionList
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
  readonly setSpecificationAttributeList = this.updater(
    (state, specificationAttributeList: SpecificationAttribute[]) => ({
      ...state,
      specificationAttributeList,
    })
  );
  readonly setSpecificationAttributeOptionList = this.updater(
    (
      state,
      specificationAttributeOptionList: SpecificationAttributeOption[]
    ) => ({
      ...state,
      specificationAttributeOptionList,
    })
  );
  readonly setSpecificationAttributeOptionGrid = this.updater(
    (state, specificationAttributeOptionGrid: PagedList<any>) => ({
      ...state,
      specificationAttributeOptionGrid,
    })
  );
}
