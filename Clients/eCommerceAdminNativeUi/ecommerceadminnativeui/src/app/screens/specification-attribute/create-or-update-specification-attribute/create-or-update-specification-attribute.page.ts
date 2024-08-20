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
import { InputComponent } from 'src/app/uı/input/input.component';
import { CreateOrUpdateSpecificationAttributeOptionComponent } from './components/create-or-update-specification-attribute-option/create-or-update-specification-attribute-option.component';
import { SpecificationAttributeService } from 'src/app/services/specification-attribute/specification-attribute.service';
import { SpecificationAttributeStore } from '../../../stores/specificationattribute.store';
import { SpecificationAttributeOptionListComponent } from './components/specification-attribute-option-list/specification-attribute-option-list.component';
import { SegmentLanguageComponent } from 'src/app/uı/segment-language/segment-language.component';

@Component({
  selector: 'app-create-or-update-specification-attribute.page',
  templateUrl: './create-or-update-specification-attribute.page.html',
  styleUrls: ['./create-or-update-specification-attribute.page.scss'],
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
    CreateOrUpdateSpecificationAttributeOptionComponent,
    SpecificationAttributeOptionListComponent,
    SegmentLanguageComponent,
  ],
})
export class CreateOrUpdatSpecificationAttributePage implements OnInit {
  title: string = 'CreateSpecificationAttribute';
  speficationAttributeForm!: FormGroup;
  speficationAttributeOptionForm!: FormGroup;
  submitted: boolean = false;
  isCreate: boolean = true;
  specificationAttributeId!: string;
  constructor(
    private formBuilder: FormBuilder,
    private activatedRoute: ActivatedRoute,
    private specificationAttributeService: SpecificationAttributeService,
    private specificationAttributeStore: SpecificationAttributeStore,
    private navCtrl: NavController
  ) {}
  initForm() {
    this.speficationAttributeForm = this.formBuilder.group({
      id: [''],
      name: ['', [Validators.required]],
      languageCode: [''],
    });
  }
  async ngOnInit() {
    this.initForm();
    this.activatedRoute.paramMap.subscribe(async (params) => {
      if (params.get('id')) {
        this.specificationAttributeId = params.get('id')!;
        await this.specificationAttributeService.getSpecificationAttributeById(
          params.get('id')!
        );
        this.speficationAttributeForm.patchValue(
          this.specificationAttributeStore.specificationAttribute$()
        );
        this.isCreate = false;
        this.title = 'UpdateSpecificationAttribute';
      } else {
        this.title = 'CreateSpecificationAttribute';
      }
    });
  }
  hasErrorSpeficationAttributeForm(controlName: string, errorName: string) {
    const control = this.speficationAttributeForm.get(controlName);
    return this.submitted && control?.hasError(errorName);
  }

  async saveSpecificationAttribute() {
    this.submitted = true;
    if (this.speficationAttributeForm.valid) {
      if (this.isCreate) {
        await this.specificationAttributeService.createSpecificationAttribute(
          this.speficationAttributeForm.value
        );
        this.navCtrl.navigateForward(
          '/create-or-update-specification-attribute/' +
            this.specificationAttributeStore.specificationAttribute$().id
        );
      } else {
        await this.specificationAttributeService.updateSpecificationAttribute(
          this.speficationAttributeForm.value
        );
        this.navCtrl.navigateForward('/specification-attribute-list');
      }
    }
  }
}
