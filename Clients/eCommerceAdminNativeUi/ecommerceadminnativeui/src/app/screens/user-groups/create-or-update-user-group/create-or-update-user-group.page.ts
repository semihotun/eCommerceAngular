import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import {
  IonContent,
  IonHeader,
  IonTitle,
  IonToolbar,
  NavController,
  IonIcon,
} from '@ionic/angular/standalone';
import { ActivatedRoute } from '@angular/router';
import { UserGroupService } from 'src/app/services/user-group/user-group.service';
import { UserGroupStore } from 'src/app/stores/user-group.store';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { HeaderComponent } from 'src/app/uı/header/header.component';
import { InputComponent } from 'src/app/uı/input/input.component';
import { BtnSubmitComponent } from 'src/app/uı/btn-submit/btn-submit.component';
import { SegmentLanguageComponent } from 'src/app/uı/segment-language/segment-language.component';
import { BehaviorSubject, Subject, takeUntil } from 'rxjs';
import { GridPostData } from 'src/app/models/core/grid';
import { UserGroupRoleService } from 'src/app/services/user-group/user-group-role.service';
import { UserGroupRoleStore } from 'src/app/stores/user-group-role.store';
import { GridComponent } from 'src/app/uı/grid/grid.component';
import { UserGroupRole } from 'src/app/models/responseModel/userGroupRole';

@Component({
  selector: 'app-create-or-update-user-group',
  templateUrl: './create-or-update-user-group.page.html',
  styleUrls: ['./create-or-update-user-group.page.scss'],
  standalone: true,
  imports: [
    IonIcon,
    IonContent,
    IonHeader,
    IonTitle,
    IonToolbar,
    CommonModule,
    FormsModule,
    HeaderComponent,
    TranslateModule,
    ReactiveFormsModule,
    InputComponent,
    BtnSubmitComponent,
    SegmentLanguageComponent,
    GridComponent,
  ],
})
export class CreateOrUpdateUserGroupPage implements OnInit, OnDestroy {
  title: 'Update User Group' | 'Create User Group' = 'Create User Group';
  form!: FormGroup;
  submitted: boolean = false;
  isCreate: boolean = true;
  onDestroy: Subject<void> = new Subject<void>();
  userGroupRole: BehaviorSubject<any> = new BehaviorSubject<any>(null);
  role: BehaviorSubject<any> = new BehaviorSubject<any>(null);
  userGroupId!: string;
  @ViewChild('roleGrid') roleGrid!: GridComponent;
  @ViewChild('userGroupRoleGrid') userGroupRoleGrid!: GridComponent;
  constructor(
    private formBuilder: FormBuilder,
    private activatedRoute: ActivatedRoute,
    private userGroupService: UserGroupService,
    private userGroupStore: UserGroupStore,
    private userGroupRoleService: UserGroupRoleService,
    private userGroupRoleStore: UserGroupRoleStore,
    private navCtrl: NavController,
    private translateService: TranslateService
  ) {}
  ngOnDestroy(): void {
    this.onDestroy.next();
    this.onDestroy.complete();
  }
  initForm() {
    this.form = this.formBuilder.group({
      id: [''],
      name: ['', [Validators.required]],
      languageCode: [''],
    });
  }
  async ngOnInit() {
    this.initForm();
    this.activatedRoute.paramMap.subscribe(async (params) => {
      if (params.get('id')) {
        this.userGroupId = params.get('id')!;
        await this.userGroupService.getUserGroupById(params.get('id')!);
        this.form.patchValue(this.userGroupStore.userGroup$());
        this.isCreate = false;
        this.title = 'Update User Group';
      } else {
        this.title = 'Create User Group';
      }
    });
    this.userGroupRoleStore.userGroupRoleGridObservable$
      .pipe(takeUntil(this.onDestroy))
      .subscribe((x) => {
        if (x) {
          this.userGroupRole.next(x);
        }
      });
    this.userGroupRoleStore.userGroupRoleNotExistRoleGridObservable$
      .pipe(takeUntil(this.onDestroy))
      .subscribe((x) => {
        if (x) {
          this.role.next(x);
        }
      });
  }
  hasError(controlName: string, errorName: string) {
    const control = this.form.get(controlName);
    return this.submitted && control?.hasError(errorName);
  }
  async submitForm() {
    this.submitted = true;
    if (this.form.valid) {
      if (!this.form.get('id')?.value) {
        await this.userGroupService.createuserGroup(this.form.value);
      } else {
        await this.userGroupService.updateUserGroup(this.form.value);
      }
      this.navCtrl.navigateForward('/user-group-list');
    }
  }
  async getAllUserGroupRoleData(data?: GridPostData) {
    this.activatedRoute.paramMap.subscribe(async (params) => {
      await this.userGroupRoleService.getUserGroupRoleGrid({
        ...data,
        userGroupId: params.get('id'),
      });
    });
  }
  async getAllNotExistRole(data?: GridPostData) {
    this.activatedRoute.paramMap.subscribe(async (params) => {
      await this.userGroupRoleService.getUserGroupNotExistRoleGrid({
        ...data,
        userGroupId: params.get('id'),
      });
    });
  }
  async addUserGroupRole(data: any) {
    let userGroupRole = new UserGroupRole();
    userGroupRole.roleId = data.id;
    userGroupRole.userGroupId = this.userGroupId;
    await this.userGroupRoleService.createUserGroupRole(userGroupRole);
    this.roleGrid.refresh();
    this.userGroupRoleGrid.refresh();
  }
  async deleteUserGroupRole(id: string) {
    await this.userGroupRoleService.deleteUserGroupRole(id);
    this.roleGrid.refresh();
    this.userGroupRoleGrid.refresh();
  }
}
