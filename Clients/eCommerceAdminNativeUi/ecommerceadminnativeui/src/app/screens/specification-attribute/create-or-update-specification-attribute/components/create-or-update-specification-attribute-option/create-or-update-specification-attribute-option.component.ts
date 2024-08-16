import { Component, Input, OnInit } from '@angular/core';
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
} from '@ionic/angular/standalone';
import { HeaderComponent } from 'src/app/uı/header/header.component';
import { BtnSubmitComponent } from 'src/app/uı/btn-submit/btn-submit.component';
import { TranslateModule } from '@ngx-translate/core';
import { InputComponent } from 'src/app/uı/input/input.component';
import { SpecificationAttributeOptionService } from './../../../../../services/specification-attribute/specification-attribute-option.service';
@Component({
  selector: 'app-create-or-update-specification-attribute-option',
  templateUrl:
    './create-or-update-specification-attribute-option.component.html',
  styleUrls: [
    './create-or-update-specification-attribute-option.component.scss',
  ],
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
export class CreateOrUpdateSpecificationAttributeOptionComponent
  implements OnInit
{
  submitted: boolean = false;
  speficationAttributeOptionForm!: FormGroup;
  @Input() specificationAttributeId!: string;
  constructor(
    private formBuilder: FormBuilder,
    private specificationAttributeOptionService: SpecificationAttributeOptionService
  ) {}
  initForm() {
    this.speficationAttributeOptionForm = this.formBuilder.group({
      id: [''],
      name: ['', [Validators.required]],
      specificationAttributeId: [''],
      languageCode: [''],
    });
  }
  async ngOnInit() {
    this.initForm();
  }
  async addSpecificationAttribute() {
    this.submitted = true;
    if (this.speficationAttributeOptionForm.valid) {
      let data = {
        ...this.speficationAttributeOptionForm.value,
        specificationAttributeId: this.specificationAttributeId,
      };
      this.specificationAttributeOptionService.createSpecificationOptionAttribute(
        data
      );
      this.speficationAttributeOptionForm.reset();
      this.submitted = false;
    }
  }
  hasErrorSpeficationAttributeOptionForm(
    controlName: string,
    errorName: string
  ) {
    const control = this.speficationAttributeOptionForm.get(controlName);
    return this.submitted && control?.hasError(errorName);
  }
}
