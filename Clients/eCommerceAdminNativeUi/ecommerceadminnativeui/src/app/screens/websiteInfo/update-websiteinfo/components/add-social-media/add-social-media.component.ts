import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { InputComponent } from 'src/app/uı/input/input.component';
import { BtnSubmitComponent } from 'src/app/uı/btn-submit/btn-submit.component';
import { WebSiteInfoService } from 'src/app/services/website-info.service';
import { WebsiteInfoStore } from 'src/app/stores/website-info.store';
import { ImagePickerComponent } from 'src/app/uı/image-picker/image-picker.component';

@Component({
  selector: 'app-add-social-media',
  templateUrl: './add-social-media.component.html',
  styleUrls: ['./add-social-media.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    TranslateModule,
    ReactiveFormsModule,
    InputComponent,
    BtnSubmitComponent,
    ImagePickerComponent,
  ],
})
export class AddSocialMediaComponent implements OnInit {
  form!: FormGroup;
  submitted: boolean = false;
  constructor(
    private formBuilder: FormBuilder,
    private webSiteInfoService: WebSiteInfoService,
    public websiteInfoStore: WebsiteInfoStore
  ) {}
  initForm() {
    this.form = this.formBuilder.group({
      platformName: ['', [Validators.required]],
      url: ['', [Validators.required]],
      icon: ['', [Validators.required]],
    });
  }
  async ngOnInit() {
    this.initForm();
  }
  hasError(controlName: string, errorName: string) {
    const control = this.form.get(controlName);
    return this.submitted && control?.hasError(errorName);
  }
  async submitForm() {
    this.submitted = true;
    if (this.form.valid) {
      let currentWebsiteInfo = this.websiteInfoStore.websiteInfo$();
      currentWebsiteInfo.socialMediaInfos.push(this.form.value);
      await this.webSiteInfoService.updateWebsiteInfo({
        ...currentWebsiteInfo,
      });
      await this.webSiteInfoService.getWebsiteInfo();
      this.submitted = false;
      this.form.reset();
      this.form.markAllAsTouched();
    }
  }
}
