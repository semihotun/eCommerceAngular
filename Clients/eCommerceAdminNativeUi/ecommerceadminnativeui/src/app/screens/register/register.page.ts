import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IonIcon, NavController } from '@ionic/angular/standalone';
import { HeaderComponent } from './../../uı/header/header.component';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { InputComponent } from 'src/app/uı/input/input.component';
import { TranslateModule } from '@ngx-translate/core';
import { BtnSubmitComponent } from 'src/app/uı/btn-submit/btn-submit.component';
import { AuthService } from 'src/app/services/auth.service';
@Component({
  selector: 'app-register',
  templateUrl: './register.page.html',
  styleUrls: ['./register.page.scss'],
  standalone: true,
  imports: [
    IonIcon,
    HeaderComponent,
    CommonModule,
    CommonModule,
    ReactiveFormsModule,
    InputComponent,
    TranslateModule,
    BtnSubmitComponent,
  ],
})
export class RegisterPage implements OnInit {
  form!: FormGroup;
  submitted: boolean = false;
  constructor(
    private formbuilder: FormBuilder,
    private authService: AuthService,
    private navCtrl: NavController
  ) {}
  initForm() {
    this.form = this.formbuilder.group(
      {
        email: ['', [Validators.required, Validators.email]],
        password: ['', Validators.required],
        passwordRepeat: ['', Validators.required],
        firstName: ['', Validators.required],
        lastName: ['', Validators.required],
      },
      { validator: passwordMatchValidator }
    );
  }
  ngOnInit() {
    this.initForm();
  }
  hasError(controlName: string, errorName: string) {
    const control = this.form.get(controlName);
    return this.submitted && control?.hasError(errorName);
  }
  async register() {
    this.submitted = true;
    if (this.form.valid) {
      await this.authService.register(this.form.value);
      this.form.reset();
      this.form.markAllAsTouched();
      this.submitted = false;
      this.navCtrl.navigateForward('home');
    }
  }
}
function passwordMatchValidator(
  control: AbstractControl
): { [key: string]: boolean } | null {
  const password = control.get('password');
  const passwordRepeat = control.get('passwordRepeat');

  if (password && passwordRepeat && password.value !== passwordRepeat.value) {
    return { passwordMismatch: true };
  }
  return null;
}
