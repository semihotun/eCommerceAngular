import { Injectable } from '@angular/core';
import { ComponentStore } from '@ngrx/component-store';
import { UserGroupRole } from '../models/responseModel/userGroupRole';
import { PagedList } from '../models/core/grid';
import { Role } from '../models/responseModel/role';

export type UserGroupRoleState = {
  userGroupRole: UserGroupRole;
  userGroupRoleList: UserGroupRole[];
  userGroupRoleGrid: PagedList<UserGroupRole>;
  userGroupRoleNotExistRoleGrid: PagedList<Role>;
};

export const userGroupRoleInitialState: UserGroupRoleState = {
  userGroupRole: new UserGroupRole(),
  userGroupRoleList: [],
  userGroupRoleGrid: new PagedList<UserGroupRole>(),
  userGroupRoleNotExistRoleGrid: new PagedList<Role>(),
};

@Injectable({
  providedIn: 'root',
})
export class UserGroupRoleStore extends ComponentStore<UserGroupRoleState> {
  readonly userGroupRole$ = this.selectSignal((x) => x.userGroupRole);
  readonly userGroupRoleList$ = this.selectSignal((x) => x.userGroupRoleList);
  readonly userGroupRoleGridObservable$ = this.select(
    (x) => x.userGroupRoleGrid
  );
  readonly userGroupRoleNotExistRoleGridObservable$ = this.select(
    (x) => x.userGroupRoleNotExistRoleGrid
  );
  constructor() {
    super(userGroupRoleInitialState);
  }

  readonly setUserGroupRole = this.updater(
    (state, userGroupRole: UserGroupRole) => ({
      ...state,
      userGroupRole,
    })
  );

  readonly setUserGroupRoleList = this.updater(
    (state, userGroupRoleList: UserGroupRole[]) => ({
      ...state,
      userGroupRoleList,
    })
  );
  readonly setUserGroupRoleGrid = this.updater(
    (state, userGroupRoleGrid: PagedList<UserGroupRole>) => ({
      ...state,
      userGroupRoleGrid,
    })
  );
  readonly setUserGroupRoleNotExistRoleGrid = this.updater(
    (state, userGroupRoleNotExistRoleGrid: PagedList<Role>) => ({
      ...state,
      userGroupRoleNotExistRoleGrid,
    })
  );
}
