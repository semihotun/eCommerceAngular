import { Injectable } from '@angular/core';
import { ComponentStore } from '@ngrx/component-store';
import { Category, CategoryTreeDTO } from '../models/responseModel/category';

export type CategorydState = {
  categoryTree: CategoryTreeDTO[];
  category: Category;
};

export const categoryInitialState: CategorydState = {
  categoryTree: [],
  category: new Category(),
};

@Injectable({
  providedIn: 'root',
})
export class CategoryStore extends ComponentStore<CategorydState> {
  readonly categoryTree$ = this.selectSignal((x) => x.categoryTree);
  readonly category$ = this.selectSignal((x) => x.category);

  constructor() {
    super(categoryInitialState);
  }

  readonly setCategoryTree = this.updater(
    (state, categoryTree: CategoryTreeDTO[]) => ({
      ...state,
      categoryTree,
    })
  );
  readonly setCategory = this.updater((state, category: Category) => ({
    ...state,
    category,
  }));
}
