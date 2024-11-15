import { Injectable, inject } from '@angular/core';
import { Result } from 'src/app/models/core/result';
import { Destroyable } from 'src/app/shared/destroyable.service';
import { environment } from 'src/environments/environment';
import { HttpService } from '../core/http.service';
import { ToastService } from '../core/toast.service';
import { UserGroupRoleStore } from 'src/app/stores/user-group-role.store';
import { UserGroupRole } from 'src/app/models/responseModel/userGroupRole';
import { PagedList } from 'src/app/models/core/grid';

@Injectable({
  providedIn: 'root',
})
export class UserGroupRoleService extends Destroyable {
  constructor() {
    super();
  }
  http = inject(HttpService);
  userGroupRoleStore = inject(UserGroupRoleStore);
  toast = inject(ToastService);

  getUserGroupNotExistRoleGrid(data?: any): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<PagedList<any>>>(
        environment.baseUrl + 'role/getusergroupnotexistrolegrid',
        data,
        this.onDestroy,
        (response) => {
          this.userGroupRoleStore.setUserGroupRoleNotExistRoleGrid(
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
  getUserGroupRoleGrid(data?: any): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<PagedList<any>>>(
        environment.baseUrl + 'userGroupRole/getgrid',
        data,
        this.onDestroy,
        (response) => {
          this.userGroupRoleStore.setUserGroupRoleGrid(response.data);
          resolve();
        },
        (err) => {
          this.toast.presentDangerToast();
          reject();
        }
      );
    });
  }
  getAllUserGroupRole(): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.get<Result<UserGroupRole[]>>(
        environment.baseUrl + 'userGroupRole/getall',
        {},
        this.onDestroy,
        (response) => {
          this.userGroupRoleStore.setUserGroupRoleList(response.data);
          resolve();
        },
        (err) => {
          reject();
        }
      );
    });
  }

  getUserGroupRoleById(id: string): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.get<Result<UserGroupRole>>(
        environment.baseUrl + 'userGroupRole/getbyid',
        { id: id },
        this.onDestroy,
        (response) => {
          console.log(response.data);
          this.userGroupRoleStore.setUserGroupRole(response.data);
          resolve();
        },
        (err) => {
          reject();
        }
      );
    });
  }
  createUserGroupRole(data: any): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<UserGroupRole>>(
        environment.baseUrl + 'userGroupRole/create',
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
  deleteUserGroupRole(id: string): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<UserGroupRole>>(
        environment.baseUrl + 'userGroupRole/delete',
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
  updateUserGroupRole(data: any): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http.post<Result<UserGroupRole>>(
        environment.baseUrl + 'userGroupRole/update',
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
