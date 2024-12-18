import { Injectable, inject } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpService } from '../core/http.service';
import { Destroyable } from 'src/app/shared/destroyable.service';
import { CategorySpecification } from 'src/app/models/responseModel/categorySpecification';
import { Result } from 'src/app/models/core/result';
import { ToastService } from '../core/toast.service';

@Injectable({
  providedIn: 'root',
})
export class CategorySpecificationService extends Destroyable {
  constructor() {
    super();
  }
  http = inject(HttpService);
  categorySpecificationStore = inject(CategorySpecificationService);
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
}
