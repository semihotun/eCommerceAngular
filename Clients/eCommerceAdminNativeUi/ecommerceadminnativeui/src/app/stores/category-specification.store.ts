import { Injectable } from '@angular/core';
import { ComponentStore } from '@ngrx/component-store';
import { PagedList } from '../models/core/grid';
import { AllNotExistSpecificationGridDTO } from '../models/responseModel/allNotExistSpecificationGridDTO';
import { AllSpecificationGridDTO } from '../models/responseModel/allSpecificationGridDTO';

export type CategorySpecificationState = {
  getAllNotExistSpecificationGridDTO: PagedList<AllNotExistSpecificationGridDTO>;
  categorySpecGrid: PagedList<AllSpecificationGridDTO>;
};

export const categorySpecificationStateInitialState: CategorySpecificationState =
  {
    getAllNotExistSpecificationGridDTO:
      new PagedList<AllNotExistSpecificationGridDTO>(),
    categorySpecGrid: new PagedList<AllSpecificationGridDTO>(),
  };

@Injectable({
  providedIn: 'root',
})
export class CategorySpecificationStore extends ComponentStore<CategorySpecificationState> {
  readonly getAllNotExistSpecificationGridDTOObservable$ = this.select(
    (x) => x.getAllNotExistSpecificationGridDTO
  );
  readonly categorySpecGridObservable$ = this.select((x) => x.categorySpecGrid);
  constructor() {
    super(categorySpecificationStateInitialState);
  }

  readonly setGetAllNotExistSpecificationGridDTO = this.updater(
    (
      state,
      getAllNotExistSpecificationGridDTO: PagedList<AllNotExistSpecificationGridDTO>
    ) => ({
      ...state,
      getAllNotExistSpecificationGridDTO,
    })
  );
  readonly setCategorySpecGrid = this.updater(
    (state, categorySpecGrid: PagedList<AllSpecificationGridDTO>) => ({
      ...state,
      categorySpecGrid,
    })
  );
}
