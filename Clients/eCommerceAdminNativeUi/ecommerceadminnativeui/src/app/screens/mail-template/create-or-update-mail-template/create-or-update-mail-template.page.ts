import { Component, OnInit } from '@angular/core';
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
} from '@ionic/angular/standalone';
import { HeaderComponent } from 'src/app/uı/header/header.component';
import { TranslateModule } from '@ngx-translate/core';
import { ActivatedRoute } from '@angular/router';
import { MailTemplateService } from 'src/app/services/mail-template.service';
import { MailTemplateStore } from 'src/app/stores/mail-template.store';
import { InputComponent } from 'src/app/uı/input/input.component';
import { BtnSubmitComponent } from 'src/app/uı/btn-submit/btn-submit.component';
import { TextEditorComponent } from 'src/app/uı/text-editor/text-editor.component';
import { Clipboard } from '@capacitor/clipboard';
import { ToastService } from 'src/app/services/core/toast.service';
@Component({
  selector: 'app-create-or-update-mail-template',
  templateUrl: './create-or-update-mail-template.page.html',
  styleUrls: ['./create-or-update-mail-template.page.scss'],
  standalone: true,
  imports: [
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
    TextEditorComponent,
  ],
})
export class CreateOrUpdateMailTemplatePage implements OnInit {
  title: string = 'Update Mail Template';
  form!: FormGroup;
  submitted: boolean = false;
  isCreate: boolean = true;
  constructor(
    private formBuilder: FormBuilder,
    private activatedRoute: ActivatedRoute,
    private mailTemplateService: MailTemplateService,
    public mailTempalteStore: MailTemplateStore,
    private navCtrl: NavController,
    private taost: ToastService
  ) {}
  initForm() {
    this.form = this.formBuilder.group({
      id: [''],
      templateHeader: [''],
      templateContent: ['', [Validators.required]],
      languageCode: [''],
    });
  }
  async ngOnInit() {
    this.initForm();
    this.activatedRoute.paramMap.subscribe(async (params) => {
      if (params.get('id')) {
        await this.mailTemplateService.getMailTemplateById(params.get('id')!);
        this.form.patchValue(this.mailTempalteStore.mailTemplate$());
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
      await this.mailTemplateService.updateMailTemplate(this.form.value);
      this.navCtrl.navigateForward('/mail-template-list');
    }
  }
  async copyClipBoard(text: string) {
    await Clipboard.write({
      string: '#{{' + text + '}}',
    });
    this.taost.presentSuccessToast('Text Copyied');
  }
}
