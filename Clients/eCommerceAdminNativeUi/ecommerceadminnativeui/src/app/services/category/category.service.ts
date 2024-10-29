import { inject, Injectable } from '@angular/core';
import { Destroyable } from 'src/app/shared/destroyable.service';
import { HttpService } from '../core/http.service';
import { ToastService } from '../core/toast.service';
import { Result } from 'src/app/models/core/result';
import { environment } from 'src/environments/environment';
import {
  Category,
  CategoryTreeDTO,
} from 'src/app/models/responseModel/category';
import { CategoryStore } from 'src/app/stores/category.store';

@Injectable({
  providedIn: 'root',
})
export class CategoryService extends Destroyable {
  constructor() {
    super();
  }
  http = inject(HttpService);
  toast = inject(ToastService);
  categoryStore = inject(CategoryStore);
  createcategory(data: any): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<Category>>(
        environment.baseUrl + 'category/create',
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
  updateCategory(data: any): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<Category>>(
        environment.baseUrl + 'category/update',
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
  getCategoryTree(): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.get<Result<CategoryTreeDTO[]>>(
        environment.baseUrl + 'category/getall',
        {},
        this.onDestroy,
        (response) => {
          this.categoryStore.setCategoryTree(response.data);
          resolve();
        },
        (err) => {
          this.toast.presentDangerToast();
          reject();
        }
      );
    });
  }
  getCategoryById(id: string): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.get<Result<Category>>(
        environment.baseUrl + 'category/getbyid',
        { id: id },
        this.onDestroy,
        (response) => {
          this.categoryStore.setCategory(response.data);
          resolve();
        },
        (err) => {
          reject();
        }
      );
    });
  }
  deleteCategory(id: string): Promise<void> {
    let data = { id: id };
    return new Promise((resolve, reject) => {
      this.http.post<Result<Category>>(
        environment.baseUrl + 'category/delete',
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
}
