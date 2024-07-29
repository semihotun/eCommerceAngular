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
import { TranslateModule } from '@ngx-translate/core';
import { ActivatedRoute } from '@angular/router';
import { InputComponent } from 'src/app/uı/input/input.component';
import { BtnSubmitComponent } from 'src/app/uı/btn-submit/btn-submit.component';
import { BrandService } from 'src/app/services/brand.service';
import { BrandStore } from 'src/app/stores/brand.store';

@Component({
  selector: 'app-createorupdatebrand',
  templateUrl: './createorupdatebrand.page.html',
  styleUrls: ['./createorupdatebrand.page.scss'],
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
  ],
})
export class CreateorupdatebrandPage implements OnInit {
  title: string = 'CreateBrand';
  form!: FormGroup;
  submitted: boolean = false;
  isCreate: boolean = true;
  constructor(
    private formBuilder: FormBuilder,
    private activatedRoute: ActivatedRoute,
    private brandService: BrandService,
    private brandStore: BrandStore,
    private navCtrl: NavController
  ) {}
  initForm() {
    this.form = this.formBuilder.group({
      id: [''],
      brandName: ['', [Validators.required]],
    });
  }
  async ngOnInit() {
    this.initForm();
    this.activatedRoute.paramMap.subscribe(async (params) => {
      if (params.get('id')) {
        await this.brandService.getBrandById(params.get('id')!);
        this.form.patchValue(this.brandStore.brand$());
        this.isCreate = false;
        this.title = 'UpdateBrand';
      } else {
        this.title = 'CreateBrand';
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
        await this.brandService.createbrand(this.form.value);
      } else {
        await this.brandService.updateBrand(this.form.value);
      }
      this.navCtrl.navigateForward('/brandlist');
    }
  }
}
