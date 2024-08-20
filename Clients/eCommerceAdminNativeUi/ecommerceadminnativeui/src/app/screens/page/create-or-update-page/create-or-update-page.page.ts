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
  IonIcon,
  NavController,
} from '@ionic/angular/standalone';
import { HeaderComponent } from 'src/app/uı/header/header.component';
import { InputComponent } from 'src/app/uı/input/input.component';
import { TranslateModule } from '@ngx-translate/core';
import { BtnSubmitComponent } from 'src/app/uı/btn-submit/btn-submit.component';
import { ActivatedRoute } from '@angular/router';
import { PageStore } from 'src/app/stores/page.store';
import { PageService } from 'src/app/services/page.service';
import { TextEditorComponent } from '../../../u\u0131/text-editor/text-editor.component';
import { SegmentLanguageComponent } from 'src/app/uı/segment-language/segment-language.component';

@Component({
  selector: 'app-create-or-update-page',
  templateUrl: './create-or-update-page.page.html',
  styleUrls: ['./create-or-update-page.page.scss'],
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
    TextEditorComponent,
    SegmentLanguageComponent,
  ],
})
export class CreateOrUpdatePagePage implements OnInit {
  title: 'Update Page' | 'Create Page' = 'Create Page';
  form!: FormGroup;
  submitted: boolean = false;
  isCreate: boolean = true;
  constructor(
    private formBuilder: FormBuilder,
    private activatedRoute: ActivatedRoute,
    private pageService: PageService,
    private pageStore: PageStore,
    private navCtrl: NavController
  ) {}
  initForm() {
    this.form = this.formBuilder.group({
      id: [''],
      pageTitle: ['', [Validators.required]],
      pageContent: ['', [Validators.required]],
      slug: [''],
      languageCode: [''],
    });
  }
  async ngOnInit() {
    this.initForm();
    this.activatedRoute.paramMap.subscribe(async (params) => {
      if (params.get('id')) {
        await this.pageService.getPageById(params.get('id')!);
        this.form.patchValue(this.pageStore.page$());
        this.isCreate = false;
        this.title = 'Update Page';
      } else {
        this.title = 'Create Page';
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
      if (this.isCreate) {
        await this.pageService.createpage(this.form.value);
      } else {
        await this.pageService.updatePage(this.form.value);
      }
      this.navCtrl.navigateForward('/page-list');
    }
  }
}
