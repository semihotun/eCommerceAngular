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
  constructor(private formBuilder: FormBuilder) {}
  initForm() {
    this.speficationAttributeOptionForm = this.formBuilder.group({
      id: [''],
      name: ['', [Validators.required]],
      specificationAttributeId: [''],
    });
  }
  async ngOnInit() {
    this.initForm();
  }
  async addSpecificationAttribute() {
    if (this.speficationAttributeOptionForm.valid) {
      let data = {
        ...this.speficationAttributeOptionForm.value,
        specificationAttributeId: this.specificationAttributeId,
      };
      console.log(data);
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