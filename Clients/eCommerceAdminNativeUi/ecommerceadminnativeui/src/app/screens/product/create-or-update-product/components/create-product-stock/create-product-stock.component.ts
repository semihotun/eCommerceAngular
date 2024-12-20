import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { GridComponent } from 'src/app/uı/grid/grid.component';
import { InputComponent } from 'src/app/uı/input/input.component';
import { IonIcon } from '@ionic/angular/standalone';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { GridPostData } from 'src/app/models/core/grid';
import { ActivatedRoute } from '@angular/router';
import { BehaviorSubject, Subject, takeUntil } from 'rxjs';
import { ProductStockService } from './../../../../../services/product/product-stock.service';
import { ProductStockStore } from './../../../../../stores/product-stock.store';
import { ProductStore } from './../../../../../stores/product.store';
import { BtnSubmitComponent } from 'src/app/uı/btn-submit/btn-submit.component';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ProductStock } from 'src/app/models/responseModel/productStock';
import { CommonModule } from '@angular/common';
import { SelectboxComponent } from './../../../../../uı/selectbox/selectbox.component';
import { WarehouseStore } from 'src/app/stores/warehouse.store';
import { WarehouseService } from 'src/app/services/warehouse.service';
import { CurrencyStore } from 'src/app/stores/currency.store';
import { CurrencyService } from './../../../../../services/currency.service';
import { DiscountType } from 'src/app/models/consts/discountTypeConst';
@Component({
  selector: 'app-create-product-stock',
  templateUrl: './create-product-stock.component.html',
  styleUrls: ['./create-product-stock.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
    GridComponent,
    InputComponent,
    TranslateModule,
    BtnSubmitComponent,
    ReactiveFormsModule,
    SelectboxComponent,
  ],
})
export class CreateProductStockComponent implements OnInit, OnDestroy {
  data: BehaviorSubject<any> = new BehaviorSubject<any>(null);
  onDestroy: Subject<void> = new Subject<void>();
  productId!: string;
  form!: FormGroup;
  submitted: boolean = false;
  discountTypeList: any = DiscountType.getAllDiscountListOfProducts(
    this.translateService
  );
  showAndHideDiscount: boolean = false;
  discountButtonText: 'Add Discount' | 'Cancel Discount' = 'Add Discount';
  @ViewChild('ProductStockGrid') productStockGrid!: GridComponent;
  constructor(
    private activatedRoute: ActivatedRoute,
    private productStockService: ProductStockService,
    private productStockStore: ProductStockStore,
    private formBuilder: FormBuilder,
    private productStore: ProductStore,
    public warehouseStore: WarehouseStore,
    public warehouseService: WarehouseService,
    private currencyService: CurrencyService,
    public currencyStore: CurrencyStore,
    private translateService: TranslateService
  ) {}

  async getAllData(data?: GridPostData) {
    this.activatedRoute.paramMap.subscribe(async (params) => {
      this.productId = params.get('id')!;
      await this.productStockService.getProductStockGrid({
        ...data,
        productId: params.get('id'),
      });
    });
  }
  hasError(controlName: string, errorName: string) {
    const control = this.form.get(controlName);
    return this.submitted && control?.hasError(errorName);
  }
  initForm() {
    this.form = this.formBuilder.group({
      id: [''],
      totalStock: [1, Validators.required],
      warehouseId: ['', Validators.required],
      currencyId: ['', Validators.required],
      languageCode: [''],
      price: [0, Validators.required],
      discountTypeId: [null],
      discountNumber: [0],
    });
  }
  async submitForm() {
    this.submitted = true;
    if (this.form.valid) {
      let productStock = new ProductStock();
      productStock = {
        ...this.form.value,
        remainingStock: this.form.get('totalStock')?.value,
        productId: this.productId,
        currencyId: this.form.get('currencyId')?.value,
        languageCode: this.productStore.product$().languageCode,
        discountTypeId: this.form.get('discountTypeId')?.value,
        discountNumber: this.form.get('discountNumber')?.value,
      };
      await this.productStockService.createproductStock(productStock);
      this.productStockGrid.refresh();
      this.form.reset();
      this.form.markAllAsTouched();
      this.submitted = false;
      this.initForm();
    }
  }
  async ngOnInit() {
    this.initForm();
    this.warehouseService.getAllWarehouse();
    this.currencyService.getAllCurrency();
    this.productStockStore.productStockGridObservable$
      .pipe(takeUntil(this.onDestroy))
      .subscribe((x) => {
        if (x) {
          this.data.next(x);
        }
      });
  }
  ngOnDestroy(): void {
    this.onDestroy.next();
    this.onDestroy.complete();
  }
  addDiscount() {
    if (!this.showAndHideDiscount) {
      this.discountButtonText = 'Cancel Discount';
    } else {
      this.form.patchValue({
        discountTypeId: null,
        discountNumber: 0,
      });
      this.discountButtonText = 'Add Discount';
    }
    this.showAndHideDiscount = !this.showAndHideDiscount;
  }
}
