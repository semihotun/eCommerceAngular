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
  IonAccordion,
  IonItem,
  IonLabel,
  IonAccordionGroup,
} from '@ionic/angular/standalone';
import { ActivatedRoute } from '@angular/router';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { ProductService } from 'src/app/services/product/product.service';
import { ProductStore } from 'src/app/stores/product.store';
import { HeaderComponent } from 'src/app/uı/header/header.component';
import { BtnSubmitComponent } from 'src/app/uı/btn-submit/btn-submit.component';
import { InputComponent } from 'src/app/uı/input/input.component';
import { SelectboxComponent } from '../../../u\u0131/selectbox/selectbox.component';
import { BrandService } from 'src/app/services/brand.service';
import { BrandStore } from 'src/app/stores/brand.store';
import { CategoryService } from 'src/app/services/category/category.service';
import { CategoryStore } from 'src/app/stores/category.store';
import { SegmentLanguageComponent } from 'src/app/uı/segment-language/segment-language.component';
import { TextEditorComponent } from 'src/app/uı/text-editor/text-editor.component';
import { CreateOrUpdateBrandPage } from '../../brand/create-or-update-brand/create-or-update-brand.page';
import { CreateOrUpdateProductSpecificationComponent } from './components/create-or-update-product-specification/create-or-update-product-specification.component';
import { CreateOrUpdateProductPhotoComponent } from './components/create-or-update-product-photo/create-or-update-product-photo.component';
import { CreateProductStockComponent } from './components/create-product-stock/create-product-stock.component';

@Component({
  selector: 'app-create-or-update-product',
  templateUrl: './create-or-update-product.page.html',
  styleUrls: ['./create-or-update-product.page.scss'],
  standalone: true,
  imports: [
    IonContent,
    IonHeader,
    IonTitle,
    IonToolbar,
    CommonModule,
    FormsModule,
    HeaderComponent,
    ReactiveFormsModule,
    TranslateModule,
    BtnSubmitComponent,
    InputComponent,
    SelectboxComponent,
    SegmentLanguageComponent,
    TextEditorComponent,
    IonAccordion,
    IonItem,
    IonLabel,
    IonAccordionGroup,
    CreateOrUpdateBrandPage,
    CreateOrUpdateProductSpecificationComponent,
    CreateOrUpdateProductPhotoComponent,
    CreateProductStockComponent,
  ],
})
export class CreateOrUpdateProductPage implements OnInit {
  title: 'Update Product' | 'Create Product' = 'Create Product';
  form!: FormGroup;
  submitted: boolean = false;
  isCreate: boolean = true;
  constructor(
    private formBuilder: FormBuilder,
    private activatedRoute: ActivatedRoute,
    private produtService: ProductService,
    private productStore: ProductStore,
    private brandService: BrandService,
    public brandStore: BrandStore,
    private categoryService: CategoryService,
    public categoryStore: CategoryStore,
    private navCtrl: NavController,
    private translateService: TranslateService
  ) {}
  initForm() {
    this.form = this.formBuilder.group({
      id: [''],
      productName: ['', [Validators.required]],
      brandId: ['', [Validators.required]],
      categoryId: ['', [Validators.required]],
      productContent: ['', [Validators.required]],
      gtin: ['', [Validators.required]],
      sku: ['', [Validators.required]],
      languageCode: [''],
    });
  }
  async ngOnInit() {
    this.initForm();
    await this.categoryService.getCategoryTree();
    await this.brandService.getAllBrand();
    this.activatedRoute.paramMap.subscribe(async (params) => {
      if (params.get('id')) {
        await this.produtService.getProductById(params.get('id')!);
        this.form.patchValue(this.productStore.product$());
        this.isCreate = false;
        this.title = 'Update Product';
      } else {
        this.title = 'Create Product';
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
        await this.produtService.createProduct(this.form.value);
      } else {
        await this.produtService.updateProduct(this.form.value);
      }
      this.navCtrl.navigateForward('/product-list');
    }
  }
}
