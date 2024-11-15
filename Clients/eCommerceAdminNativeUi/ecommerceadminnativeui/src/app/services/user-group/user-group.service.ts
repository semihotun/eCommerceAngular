import { Injectable, inject } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpService } from '../core/http.service';
import { UserGroup } from '../../models/responseModel/userGroup';
import { Result } from '../../models/core/result';
import { Destroyable } from '../../shared/destroyable.service';
import { ToastService } from '../core/toast.service';
import { UserGroupStore } from '../../stores/user-group.store';

@Injectable({
  providedIn: 'root',
})
export class UserGroupService extends Destroyable {
  constructor() {
    super();
  }
  http = inject(HttpService);
  userGroupStore = inject(UserGroupStore);
  toast = inject(ToastService);

  getAllUserGroup(): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.get<Result<UserGroup[]>>(
        environment.baseUrl + 'userGroup/getall',
        {},
        this.onDestroy,
        (response) => {
          this.userGroupStore.setUserGroupList(response.data);
          resolve();
        },
        (err) => {
          reject();
        }
      );
    });
  }

  getUserGroupById(id: string): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.get<Result<UserGroup>>(
        environment.baseUrl + 'userGroup/getbyid',
        { id: id },
        this.onDestroy,
        (response) => {
          this.userGroupStore.setUserGroup(response.data);
          resolve();
        },
        (err) => {
          reject();
        }
      );
    });
  }
  createuserGroup(data: any): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<UserGroup>>(
        environment.baseUrl + 'userGroup/create',
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
  updateUserGroup(data: any): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<UserGroup>>(
        environment.baseUrl + 'userGroup/update',
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
