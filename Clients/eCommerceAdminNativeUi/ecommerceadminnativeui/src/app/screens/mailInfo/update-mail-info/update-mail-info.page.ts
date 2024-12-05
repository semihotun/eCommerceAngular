import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { HeaderComponent } from 'src/app/uı/header/header.component';
import { TranslateModule } from '@ngx-translate/core';
import { InputComponent } from 'src/app/uı/input/input.component';
import { BtnSubmitComponent } from 'src/app/uı/btn-submit/btn-submit.component';
import { ActivatedRoute } from '@angular/router';
import { MailInfoStore } from 'src/app/stores/mail-info.store';
import { MailInfoService } from 'src/app/services/mail-info.service';

@Component({
  selector: 'app-update-mail-info',
  templateUrl: './update-mail-info.page.html',
  styleUrls: ['./update-mail-info.page.scss'],
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    HeaderComponent,
    TranslateModule,
    ReactiveFormsModule,
    InputComponent,
    BtnSubmitComponent,
  ],
})
export class UpdateMailInfoPage implements OnInit {
  form!: FormGroup;
  submitted: boolean = false;
  constructor(
    private formBuilder: FormBuilder,
    private activatedRoute: ActivatedRoute,
    private mailInfoService: MailInfoService,
    public mailInfoStore: MailInfoStore
  ) {}
  initForm() {
    this.form = this.formBuilder.group({
      fromAddress: ['', [Validators.required]],
      fromPassword: ['', [Validators.required]],
      host: ['', [Validators.required]],
      port: [0, [Validators.required]],
    });
  }
  async ngOnInit() {
    this.initForm();
    this.activatedRoute.paramMap.subscribe(async (params) => {
      await this.mailInfoService.getMailInfo();
      console.log(this.mailInfoStore.mailInfo$());
      this.form.patchValue(this.mailInfoStore.mailInfo$());
    });
  }
  hasError(controlName: string, errorName: string) {
    const control = this.form.get(controlName);
    return this.submitted && control?.hasError(errorName);
  }
  async submitForm() {
    this.submitted = true;
    if (this.form.valid) {
      await this.mailInfoService.updateMailInfo(this.form.value);
    }
  }
}
