import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { NavController, IonContent } from '@ionic/angular/standalone';
import { HeaderComponent } from 'src/app/uı/header/header.component';
import { FooterComponent } from 'src/app/uı/footer/footer.component';
import { GlobalService } from 'src/app/services/core/global.service';
import { MobileFooterComponent } from 'src/app/uı/mobile-footer/mobile-footer.component';
import { InputComponent } from 'src/app/uı/input/input.component';
import { TranslateModule } from '@ngx-translate/core';
import { BtnSubmitComponent } from 'src/app/uı/btn-submit/btn-submit.component';
import { CheckboxComponent } from 'src/app/uı/checkbox/checkbox.component';
import { BehaviorSubject, Observable } from 'rxjs';
import { RouterModule, ActivatedRoute } from '@angular/router';
import { UserService } from 'src/app/services/user.service';
import { SubHeaderComponent } from 'src/app/uı/sub-header/sub-header.component';

@Component({
  selector: 'app-login',
  templateUrl: './login.page.html',
  styleUrls: ['./login.page.scss'],
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
export class LoginPage implements OnInit {
  form!: FormGroup;
  submitted: boolean = false;
  passwordTypeCheck: boolean = false;
  passwordType: BehaviorSubject<'text' | 'password'> = new BehaviorSubject<
    'text' | 'password'
  >('password');
  glb = inject(GlobalService);
  activationCode!: string;
  constructor(
    private formBuilder: FormBuilder,
    private userService: UserService,
    private navController: NavController,
    private activatedRoute: ActivatedRoute
  ) {
    this.initForm();
  }
  initForm() {
    this.form = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
      rememberMe: [true, Validators.required],
    });
  }
  async saveForm() {
    await this.userService.login(this.form.value).then(() => {
      if (this.activationCode) {
        this.navController.navigateForward([
          'activation-code',
          localStorage.getItem('activationCode'),
        ]);
      }
      this.navController.navigateForward('');
    });
  }
  async ngOnInit() {
    await this.activatedRoute.paramMap.subscribe(async (params) => {
      this.activationCode = params.get('activationCode')!;
    });
  }
  changePasswordType() {
    this.passwordTypeCheck = !this.passwordTypeCheck;
    this.passwordType.next(this.passwordTypeCheck ? 'text' : 'password');
    let passwordDom = document.getElementById('password') as HTMLInputElement;
    passwordDom.type = this.passwordType.value;
  }
  hasError(controlName: string, errorName: string) {
    const control = this.form.get(controlName);
    return this.submitted && control?.hasError(errorName);
  }
}
