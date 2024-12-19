import { Injectable, inject } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpService } from '../core/http.service';
import { Destroyable } from 'src/app/shared/destroyable.service';
import { CategorySpecification } from 'src/app/models/responseModel/categorySpecification';
import { Result } from 'src/app/models/core/result';
import { ToastService } from '../core/toast.service';
import { GridPostData, PagedList } from './../../models/core/grid';
import { CategorySpecificationStore } from 'src/app/stores/category-specification.store';
import { AllNotExistSpecificationGridDTO } from 'src/app/models/responseModel/allNotExistSpecificationGridDTO';

@Injectable({
  providedIn: 'root',
})
export class CategorySpecificationService extends Destroyable {
  constructor() {
    super();
  }
  http = inject(HttpService);
  categorySpecificationStore = inject(CategorySpecificationStore);
  toast = inject(ToastService);

  createCategorySpecification(data: any): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<CategorySpecification>>(
        environment.baseUrl + 'categorySpecification/create',
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

  getAllNotExistSpecificationGridDTO(
    categoryId: any,
    data: GridPostData
  ): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<PagedList<AllNotExistSpecificationGridDTO>>>(
        environment.baseUrl +
          'categorySpecification/getallnotexistspecificationgrid',
        { ...data, categoryId: categoryId },
        this.onDestroy,
        (response) => {
          this.categorySpecificationStore.setGetAllNotExistSpecificationGridDTO(
            response.data
          );
          resolve();
        },
        (err) => {
          this.toast.presentDangerToast();
          reject();
        }
      );
    });
  }
  deleteCategorySpecificationAttribute(id: string): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<PagedList<AllNotExistSpecificationGridDTO>>>(
        environment.baseUrl + 'categorySpecification/delete',
        { id: id },
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
  getAllSpecificationGridDTO(
    categoryId: any,
    data: GridPostData
  ): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<PagedList<AllNotExistSpecificationGridDTO>>>(
        environment.baseUrl + 'categorySpecification/getgrid',
        { ...data, categoryId: categoryId },
        this.onDestroy,
        (response) => {
          this.categorySpecificationStore.setCategorySpecGrid(response.data);
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
