import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { NavController } from '@ionic/angular/standalone';
import { HeaderComponent } from 'src/app/uı/header/header.component';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { ActivatedRoute } from '@angular/router';
import { InputComponent } from 'src/app/uı/input/input.component';
import { BtnSubmitComponent } from 'src/app/uı/btn-submit/btn-submit.component';
import { SegmentLanguageComponent } from 'src/app/uı/segment-language/segment-language.component';
import { DiscountService } from 'src/app/services/discount/discount.service';
import { DiscountStore } from 'src/app/stores/discount.store';
import { SelectboxComponent } from 'src/app/uı/selectbox/selectbox.component';
import { DiscountType } from 'src/app/models/consts/discountTypeConst';
import { CategoryService } from 'src/app/services/category/category.service';
import { CategoryStore } from 'src/app/stores/category.store';

@Component({
  selector: 'app-create-or-update-discount',
  templateUrl: './create-or-update-discount.page.html',
  styleUrls: ['./create-or-update-discount.page.scss'],
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    HeaderComponent,
    TranslateModule,
    ReactiveFormsModule,
    InputComponent,
    BtnSubmitComponent,
    SegmentLanguageComponent,
    SelectboxComponent,
  ],
})
export class CreateOrUpdateDiscountPage implements OnInit {
  title: 'Discount Info' | 'Create Discount' = 'Discount Info';
  form!: FormGroup;
  submitted: boolean = false;
  isCreate: boolean = true;
  isCategoryDiscount: boolean = false;
  discountTypeList: any = DiscountType.getAllNonProductDiscounts(
    this.translateService
  );
  constructor(
    private formBuilder: FormBuilder,
    private activatedRoute: ActivatedRoute,
    private discountService: DiscountService,
    private discountStore: DiscountStore,
    private navCtrl: NavController,
    private translateService: TranslateService,
    private categoryService: CategoryService,
    public categoryStore: CategoryStore
  ) {}
  initForm() {
    this.form = this.formBuilder.group({
      id: [''],
      name: ['', [Validators.required]],
      discountTypeId: ['', [Validators.required]],
      categoryId: [null],
      discountNumber: [0, [Validators.required]],
    });
  }
  async ngOnInit() {
    this.initForm();
    await this.categoryService.getCategoryTree();
    this.activatedRoute.paramMap.subscribe(async (params) => {
      if (params.get('id')) {
        await this.discountService.getDiscountById(params.get('id')!);
        this.form.patchValue(this.discountStore.discount$());
        this.isCreate = false;
        this.title = 'Discount Info';
      } else {
        this.title = 'Create Discount';
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
        await this.discountService.creatediscount(this.form.value);
      }
      this.navCtrl.navigateForward('/discount-list');
    }
  }
  discountTypeChange(data: any) {
    if (
      data === DiscountType.CategoryCurrencyDiscount.id ||
      data === DiscountType.CategoryPercentDiscount.id
    ) {
      this.isCategoryDiscount = true;
    } else {
      this.isCategoryDiscount = false;
      this.form.get('discountTypeId')?.patchValue('');
    }
  }
}
