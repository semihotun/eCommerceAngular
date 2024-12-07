import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  ValidationErrors,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { IonContent, NavController } from '@ionic/angular/standalone';
import { HeaderComponent } from 'src/app/uı/header/header.component';
import { FooterComponent } from 'src/app/uı/footer/footer.component';
import { MobileFooterComponent } from 'src/app/uı/mobile-footer/mobile-footer.component';
import { InputComponent } from 'src/app/uı/input/input.component';
import { TranslateModule } from '@ngx-translate/core';
import { BtnSubmitComponent } from 'src/app/uı/btn-submit/btn-submit.component';
import { GlobalService } from 'src/app/services/core/global.service';
import { CheckboxComponent } from '../../u\u0131/checkbox/checkbox.component';
import { RouterModule } from '@angular/router';
import { CustomerUserService } from 'src/app/services/customer-user.service';
import { SubHeaderComponent } from 'src/app/uı/sub-header/sub-header.component';
@Component({
  selector: 'app-register',
  templateUrl: './register.page.html',
  styleUrls: ['./register.page.scss'],
  standalone: true,
  imports: [
    IonContent,
    CommonModule,
    FormsModule,
    HeaderComponent,
    FooterComponent,
    MobileFooterComponent,
    InputComponent,
    TranslateModule,
    ReactiveFormsModule,
    BtnSubmitComponent,
    CheckboxComponent,
    RouterModule,
    SubHeaderComponent,
  ],
})
export class RegisterPage implements OnInit {
  form!: FormGroup;
  glb = inject(GlobalService);
  userService = inject(CustomerUserService);
  submitted: boolean = false;
  constructor(
    private formBuilder: FormBuilder,
    private navController: NavController
  ) {
    this.initForm();
  }
  ngOnInit() {}
  initForm() {
    this.form = this.formBuilder.group({
      firstName: ['', [Validators.required]],
      lastName: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      password: [''.trim(), [Validators.required]],
      termsOfUse: [false, this.trueValidator()],
    });
  }

  trueValidator(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      return control.value === true ? null : { termOfUseFalse: true };
    };
  }
  async saveForm() {
    this.submitted = true;
    if (this.form.valid) {
      await this.userService.register(this.form.value).then(() => {
        this.navController.navigateForward('');
      });
    }
  }
  hasError(controlName: string, errorName: string) {
    const control = this.form.get(controlName);
    return this.submitted && control?.hasError(errorName);
  }
}
