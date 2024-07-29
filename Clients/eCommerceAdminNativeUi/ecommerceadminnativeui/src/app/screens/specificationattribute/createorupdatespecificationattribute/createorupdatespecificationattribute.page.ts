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
  NavController,
  IonHeader,
  IonTitle,
  IonToolbar,
} from '@ionic/angular/standalone';
import { HeaderComponent } from 'src/app/uı/header/header.component';
import { BtnSubmitComponent } from 'src/app/uı/btn-submit/btn-submit.component';
import { TranslateModule } from '@ngx-translate/core';
import { ActivatedRoute } from '@angular/router';
import { SpecificationAttributeService } from 'src/app/services/specificationattribute.service';
import { SpecificationAttributeStore } from 'src/app/stores/specificationattribute.store';
import { InputComponent } from 'src/app/uı/input/input.component';

@Component({
  selector: 'app-createorupdatespecificationattribute',
  templateUrl: './createorupdatespecificationattribute.page.html',
  styleUrls: ['./createorupdatespecificationattribute.page.scss'],
  standalone: true,
  imports: [
    IonContent,
    IonHeader,
    IonTitle,
    IonToolbar,
    CommonModule,
    FormsModule,
    HeaderComponent,
    BtnSubmitComponent,
    TranslateModule,
    ReactiveFormsModule,
    InputComponent,
  ],
})
export class CreateorupdatespecificationattributePage implements OnInit {
  title: string = 'CreateSpecificationAttribute';
  form!: FormGroup;
  submitted: boolean = false;
  isCreate: boolean = true;
  constructor(
    private formBuilder: FormBuilder,
    private activatedRoute: ActivatedRoute,
    private specificationAttributeService: SpecificationAttributeService,
    private specificationAttributeStore: SpecificationAttributeStore,
    private navCtrl: NavController
  ) {}
  initForm() {
    this.form = this.formBuilder.group({
      id: [''],
      name: ['', [Validators.required]],
    });
  }
  async ngOnInit() {
    this.initForm();
    this.activatedRoute.paramMap.subscribe(async (params) => {
      if (params.get('id')) {
        await this.specificationAttributeService.getSpecificationAttributeById(
          params.get('id')!
        );
        this.form.patchValue(this.specificationAttributeStore.brand$());
        this.isCreate = false;
        this.title = 'UpdateSpecificationAttribute';
      } else {
        this.title = 'CreateSpecificationAttribute';
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
        await this.specificationAttributeService.createSpecificationAttribute(
          this.form.value
        );
      } else {
        await this.specificationAttributeService.updateSpecificationAttribute(
          this.form.value
        );
      }
      this.navCtrl.navigateForward('/specificationattributelist');
    }
  }
}
