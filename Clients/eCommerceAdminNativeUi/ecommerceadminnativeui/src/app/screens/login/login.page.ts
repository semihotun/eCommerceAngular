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
  selector: 'app-login',
  templateUrl: './login.page.html',
  styleUrls: ['./login.page.scss'],
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
export class LoginPage implements OnInit {
  form!: FormGroup;
  submitted: boolean = false;
  constructor(
    private formbuilder: FormBuilder,
    private authService: AuthService,
    private navCtrl: NavController
  ) {}
  initForm() {
    this.form = this.formbuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
    });
  }
  ngOnInit() {
    this.initForm();
  }
  hasError(controlName: string, errorName: string) {
    const control = this.form.get(controlName);
    return this.submitted && control?.hasError(errorName);
  }
  async login() {
    this.submitted = true;
    if (this.form.valid) {
      await this.authService.login(this.form.value);
      this.form.reset();
      this.form.markAllAsTouched();
      this.submitted = false;
      this.navCtrl.navigateForward('home');
    }
  }
}
