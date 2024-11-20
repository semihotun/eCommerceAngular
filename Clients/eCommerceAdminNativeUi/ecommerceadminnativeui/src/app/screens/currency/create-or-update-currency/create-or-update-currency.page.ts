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
import { BtnSubmitComponent } from 'src/app/uı/btn-submit/btn-submit.component';
import { CurrencyService } from 'src/app/services/currency.service';
import { CurrencyStore } from 'src/app/stores/currency.store';
import { SegmentLanguageComponent } from 'src/app/uı/segment-language/segment-language.component';

@Component({
  selector: 'app-create-or-update-currency',
  templateUrl: './create-or-update-currency.page.html',
  styleUrls: ['./create-or-update-currency.page.scss'],
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
  ],
})
export class CreateOrUpdateCurrencyPage implements OnInit {
  title: 'Update Currency' | 'Create Currency' = 'Create Currency';
  form!: FormGroup;
  submitted: boolean = false;
  isCreate: boolean = true;
  constructor(
    private formBuilder: FormBuilder,
    private activatedRoute: ActivatedRoute,
    private currencyService: CurrencyService,
    private currencyStore: CurrencyStore,
    private navCtrl: NavController,
    private translateService: TranslateService
  ) {}
  initForm() {
    this.form = this.formBuilder.group({
      id: [''],
      symbol: ['', [Validators.required]],
      code: ['', [Validators.required]],
      name: ['', [Validators.required]],
      languageCode: [''],
    });
  }
  async ngOnInit() {
    this.initForm();
    this.activatedRoute.paramMap.subscribe(async (params) => {
      if (params.get('id')) {
        console.log('act', this.translateService.currentLang);
        await this.currencyService.getCurrencyById(params.get('id')!);
        this.form.patchValue(this.currencyStore.currency$());
        this.isCreate = false;
        this.title = 'Update Currency';
      } else {
        this.title = 'Create Currency';
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
        await this.currencyService.createcurrency(this.form.value);
      } else {
        await this.currencyService.updateCurrency(this.form.value);
      }
      this.navCtrl.navigateForward('/currency-list');
    }
  }
}
