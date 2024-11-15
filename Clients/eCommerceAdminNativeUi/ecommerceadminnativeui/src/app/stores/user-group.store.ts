import { Injectable } from '@angular/core';
import { ComponentStore } from '@ngrx/component-store';
import { UserGroup } from '../models/responseModel/userGroup';

export type UserGroupState = {
  userGroup: UserGroup;
  userGroupList: UserGroup[];
};

export const userGroupInitialState: UserGroupState = {
  userGroup: new UserGroup(),
  userGroupList: [],
};

@Injectable({
  providedIn: 'root',
})
export class UserGroupStore extends ComponentStore<UserGroupState> {
  readonly userGroup$ = this.selectSignal((x) => x.userGroup);
  readonly userGroupList$ = this.selectSignal((x) => x.userGroupList);
  constructor() {
    super(userGroupInitialState);
  }

  readonly setUserGroup = this.updater((state, userGroup: UserGroup) => ({
    ...state,
    userGroup,
  }));

  readonly setUserGroupList = this.updater(
    (state, userGroupList: UserGroup[]) => ({
      ...state,
      userGroupList,
    })
  );
}
