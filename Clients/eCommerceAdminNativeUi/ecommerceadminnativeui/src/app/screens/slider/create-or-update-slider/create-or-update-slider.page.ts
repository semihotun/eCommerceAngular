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
import { BtnSubmitComponent } from 'src/app/uı/btn-submit/btn-submit.component';
import { TranslateModule } from '@ngx-translate/core';
import { ActivatedRoute } from '@angular/router';
import { InputComponent } from 'src/app/uı/input/input.component';
import { SliderService } from 'src/app/services/slider.service';
import { SliderStore } from 'src/app/stores/slider.store';
import { ImagePickerComponent } from '../../../u\u0131/image-picker/image-picker.component';
import { SegmentLanguageComponent } from 'src/app/uı/segment-language/segment-language.component';

@Component({
  selector: 'app-create-or-update-slider',
  templateUrl: './create-or-update-slider.page.html',
  styleUrls: ['./create-or-update-slider.page.scss'],
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    HeaderComponent,
    BtnSubmitComponent,
    TranslateModule,
    InputComponent,
    ReactiveFormsModule,
    ImagePickerComponent,
    SegmentLanguageComponent,
  ],
})
export class CreateOrUpdateSliderPage implements OnInit {
  title: 'Update Slider' | 'Create Slider' = 'Create Slider';
  form!: FormGroup;
  submitted: boolean = false;
  isCreate: boolean = true;
  constructor(
    private formBuilder: FormBuilder,
    private activatedRoute: ActivatedRoute,
    private sliderService: SliderService,
    private sliderStore: SliderStore,
    private navCtrl: NavController
  ) {}

  ngOnInit() {
    this.initForm();
    this.activatedRoute.paramMap.subscribe(async (params) => {
      if (params.get('id')) {
        await this.sliderService.getSliderById(params.get('id')!);
        this.form.patchValue(this.sliderStore.slider$());
        this.isCreate = false;
        this.title = 'Update Slider';
      } else {
        this.title = 'Create Slider';
      }
    });
  }
  initForm() {
    this.form = this.formBuilder.group({
      id: [''],
      sliderImage: ['', [Validators.required]],
      sliderHeading: ['', [Validators.required]],
      sliderText: ['', [Validators.required]],
      sliderLink: ['', [Validators.required]],
      languageCode: [''],
    });
  }
  async submitForm() {
    this.submitted = true;
    if (this.form.valid) {
      if (this.isCreate) {
        await this.sliderService.createSlider(this.form.value);
      } else {
        await this.sliderService.updateSlider(this.form.value);
      }
      this.navCtrl.navigateForward('/slider-list');
    }
  }
  hasError(controlName: string, errorName: string) {
    const control = this.form.get(controlName);
    return this.submitted && control?.hasError(errorName);
  }
}
