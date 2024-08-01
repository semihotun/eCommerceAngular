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
  ModalController,
  IonToolbar,
  IonItem,
  IonButtons,
  IonButton,
  IonIcon,
} from '@ionic/angular/standalone';
import { CategoryService } from 'src/app/services/category/category.service';
import { CategoryStore } from 'src/app/stores/category.store';
import { HeaderComponent } from 'src/app/uı/header/header.component';
import { BtnSubmitComponent } from 'src/app/uı/btn-submit/btn-submit.component';
import { TranslateModule } from '@ngx-translate/core';
import { InputComponent } from 'src/app/uı/input/input.component';

@Component({
  selector: 'app-create-or-update-category',
  templateUrl: './create-or-update-category.page.html',
  styleUrls: ['./create-or-update-category.page.scss'],
  standalone: true,
  imports: [
    IonItem,
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
    IonButton,
    IonButtons,
    IonIcon,
  ],
})
export class CreateOrUpdateCategoryPage implements OnInit {
  title: 'Update Category' | 'Create Category' = 'Create Category';
  form!: FormGroup;
  submitted: boolean = false;
  isCreate: boolean = true;
  @Input() parentCategoryId!: string;
  @Input() Id!: string;
  constructor(
    private formBuilder: FormBuilder,
    private categoryService: CategoryService,
    private categoryStore: CategoryStore,
    private modalController: ModalController
  ) {}
  initForm() {
    this.form = this.formBuilder.group({
      id: [''],
      categoryName: ['', [Validators.required]],
      parentCategoryId: [null],
    });
  }
  async ngOnInit() {
    this.initForm();
    if (this.Id) {
      await this.categoryService.getCategoryById(this.Id);
      this.form.patchValue(this.categoryStore.category$());
      this.isCreate = false;
      this.title = 'Update Category';
    } else {
      this.title = 'Create Category';
    }
  }
  hasError(controlName: string, errorName: string) {
    const control = this.form.get(controlName);
    return this.submitted && control?.hasError(errorName);
  }
  async submitForm() {
    this.submitted = true;
    if (this.form.valid) {
      let data = {
        ...this.form.value,
        parentCategoryId: this.parentCategoryId,
      };
      if (this.isCreate) {
        await this.categoryService.createcategory(data);
      } else {
        await this.categoryService.updateCategory(data);
      }
      await this.categoryService.getCategoryTree();
      this.modalController.dismiss();
    }
  }
  cancel() {
    this.modalController.dismiss();
  }
}
