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
import { ActivatedRoute } from '@angular/router';
import { InputComponent } from 'src/app/uı/input/input.component';
import { BtnSubmitComponent } from 'src/app/uı/btn-submit/btn-submit.component';
import { WebSiteInfoService } from 'src/app/services/website-info.service';
import { WebsiteInfoStore } from 'src/app/stores/website-info.store';
import { ImagePickerComponent } from '../../../u\u0131/image-picker/image-picker.component';
import { SocialMediaListComponent } from './components/social-media-list/social-media-list.component';
import { AddSocialMediaComponent } from './components/add-social-media/add-social-media.component';

@Component({
  selector: 'app-update-websiteinfo',
  templateUrl: './update-websiteinfo.page.html',
  styleUrls: ['./update-websiteinfo.page.scss'],
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    HeaderComponent,
    TranslateModule,
    ReactiveFormsModule,
    InputComponent,
    BtnSubmitComponent,
    ImagePickerComponent,
    SocialMediaListComponent,
    AddSocialMediaComponent,
  ],
})
export class UpdateWebsiteinfoPage implements OnInit {
  form!: FormGroup;
  submitted: boolean = false;
  constructor(
    private formBuilder: FormBuilder,
    private activatedRoute: ActivatedRoute,
    private webSiteInfoService: WebSiteInfoService,
    public websiteInfoStore: WebsiteInfoStore
  ) {}
  initForm() {
    this.form = this.formBuilder.group({
      socialMediaText: ['', [Validators.required]],
      logo: ['', [Validators.required]],
      webSiteName: ['', [Validators.required]],
    });
  }
  async ngOnInit() {
    this.initForm();
    this.activatedRoute.paramMap.subscribe(async (params) => {
      await this.webSiteInfoService.getWebsiteInfo();
      this.form.patchValue(this.websiteInfoStore.websiteInfo$());
    });
  }
  hasError(controlName: string, errorName: string) {
    const control = this.form.get(controlName);
    return this.submitted && control?.hasError(errorName);
  }
  async submitForm() {
    this.submitted = true;
    if (this.form.valid) {
      let updateData = {
        ...this.websiteInfoStore.websiteInfo$(),
        ...this.form.value,
      };
      await this.webSiteInfoService.updateWebsiteInfo(updateData);
    }
  }
}
