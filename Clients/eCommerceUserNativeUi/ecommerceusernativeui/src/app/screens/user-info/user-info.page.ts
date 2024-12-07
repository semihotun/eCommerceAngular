import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { IonContent } from '@ionic/angular/standalone';
import { HeaderComponent } from '../../u\u0131/header/header.component';
import { SubHeaderComponent } from '../../u\u0131/sub-header/sub-header.component';
import { FooterComponent } from '../../u\u0131/footer/footer.component';
import { MobileFooterComponent } from '../../u\u0131/mobile-footer/mobile-footer.component';
import { TranslateModule } from '@ngx-translate/core';
import { GlobalService } from 'src/app/services/core/global.service';
import { CustomerUserService } from 'src/app/services/customer-user.service';
import { CustomerUserStore } from 'src/app/stores/customer-user.store';
import { BtnSubmitComponent } from '../../u\u0131/btn-submit/btn-submit.component';
import { InputComponent } from 'src/app/uı/input/input.component';

@Component({
  selector: 'app-user-info',
  templateUrl: './user-info.page.html',
  styleUrls: ['./user-info.page.scss'],
  standalone: true,
  imports: [
    IonContent,
    TranslateModule,
    CommonModule,
    FormsModule,
    HeaderComponent,
    SubHeaderComponent,
    FooterComponent,
    MobileFooterComponent,
    BtnSubmitComponent,
    TranslateModule,
    InputComponent,
    ReactiveFormsModule,
  ],
})
export class UserInfoPage implements OnInit {
  form!: FormGroup;
  submitted: boolean = false;
  constructor(
    public glb: GlobalService,
    private userService: CustomerUserService,
    public userStore: CustomerUserStore,
    private formBuilder: FormBuilder
  ) {}

  async ngOnInit() {
    this.initForm();
    await this.userService.getCustomerUserDto().then(() => {
      this.form.patchValue({
        ...this.userStore.customerUserDto$(),
        createdOnUtc: this.userStore
          .customerUserDto$()
          .createdOnUtc.split('T')[0],
      });
    });
  }

  initForm() {
    this.form = this.formBuilder.group({
      id: [''],
      firstName: ['', [Validators.required]],
      lastName: ['', [Validators.required]],
      email: [''],
      createdOnUtc: [],
    });
  }
  hasError(controlName: string, errorName: string) {
    const control = this.form.get(controlName);
    return this.submitted && control?.hasError(errorName);
  }
  submitForm() {
    this.submitted = true;
    if (this.form.valid) {
      this.userService.updateCustomerUser(this.form.value);
      this.submitted = false;
    }
  }
  verifyAccount() {
    this.userService.verifyAccount();
  }
}
