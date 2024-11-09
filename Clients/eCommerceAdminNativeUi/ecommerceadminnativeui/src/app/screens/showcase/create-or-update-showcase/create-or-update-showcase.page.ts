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
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { ActivatedRoute } from '@angular/router';
import { InputComponent } from 'src/app/uı/input/input.component';
import { SegmentLanguageComponent } from 'src/app/uı/segment-language/segment-language.component';
import { ShowcaseService } from 'src/app/services/showcase/showcase.service';
import { BtnSubmitComponent } from 'src/app/uı/btn-submit/btn-submit.component';
import { ShowcaseStore } from 'src/app/stores/showcase.store';
import { ShowcaseTypeService } from 'src/app/services/showcase/showcase-type.service';
import { ShowCaseType } from 'src/app/models/responseModel/showcaseType';
import { SelectboxComponent } from 'src/app/uı/selectbox/selectbox.component';
import { ShowcaseProductComponent } from './components/showcase-product/showcase-product.component';
import { ShowcaseConst } from 'src/app/models/consts/showcaseConst';
import { TextEditorComponent } from 'src/app/uı/text-editor/text-editor.component';
@Component({
  selector: 'app-create-or-update-showcase',
  templateUrl: './create-or-update-showcase.page.html',
  styleUrls: ['./create-or-update-showcase.page.scss'],
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
    SelectboxComponent,
    ShowcaseProductComponent,
    TextEditorComponent,
  ],
})
export class CreateOrUpdateShowcasePage implements OnInit {
  title: 'Update Showcase' | 'Create Showcase' = 'Create Showcase';
  form!: FormGroup;
  submitted: boolean = false;
  isCreate: boolean = true;
  showcaseTypes!: ShowCaseType[];
  isText: boolean = false;
  constructor(
    private formBuilder: FormBuilder,
    private activatedRoute: ActivatedRoute,
    private showcaseService: ShowcaseService,
    private showcaseStore: ShowcaseStore,
    private navCtrl: NavController,
    private translateService: TranslateService,
    private showcaseTypeService: ShowcaseTypeService
  ) {}
  initForm() {
    this.form = this.formBuilder.group({
      id: [''],
      showCaseOrder: [1, [Validators.required]],
      showCaseTitle: ['', [Validators.required]],
      showCaseTypeId: ['', [Validators.required]],
      showCaseText: [null],
      languageCode: [''],
    });
  }
  async ngOnInit() {
    this.initForm();
    this.activatedRoute.paramMap.subscribe(async (params) => {
      await this.showcaseTypeService.getAllShowCaseType();
      this.showcaseTypes = this.showcaseStore.showcaseTypeList$();
      if (params.get('id')) {
        await this.showcaseService.getShowcaseById(params.get('id')!);
        this.form.patchValue(this.showcaseStore.showcase$());
        this.isCreate = false;
        this.title = 'Update Showcase';
      } else {
        this.title = 'Create Showcase';
      }
      this.isTextOrProduct();
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
        await this.showcaseService.createShowcase(this.form.value);
      } else {
        await this.showcaseService.updateShowcase(this.form.value);
      }
      this.navCtrl.navigateForward('/showcase-list');
    }
  }
  isTextOrProduct() {
    this.isText =
      this.form.get('showCaseTypeId')?.value == ShowcaseConst.Text.id;
  }
}
