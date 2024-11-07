import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { ProductStore } from 'src/app/stores/product.store';
import { ProductSpecification } from './../../../../../models/responseModel/productSpecification';
import { ProductSpecificationService } from './../../../../../services/product/product-specification.service';
import { GridComponent } from '../../../../../u\u0131/grid/grid.component';
import { GridPostData } from 'src/app/models/core/grid';
import { BehaviorSubject, filter, take, pipe, takeUntil, Subject } from 'rxjs';
import { SpecificationAttributeService } from './../../../../../services/specification-attribute/specification-attribute.service';
import { SpecificationAttributeStore } from 'src/app/stores/specificationattribute.store';
import {
  SpecificationAttribute,
  SpecificationAttributeOption,
} from 'src/app/models/responseModel/specificationattribute';
import { SelectboxComponent } from '../../../../../u\u0131/selectbox/selectbox.component';
import { TranslateModule } from '@ngx-translate/core';
import { SpecificationAttributeOptionService } from 'src/app/services/specification-attribute/specification-attribute-option.service';
import { BtnSubmitComponent } from '../../../../../u\u0131/btn-submit/btn-submit.component';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ProductSpecificationStore } from 'src/app/stores/productSpecification.store';
import { ActivatedRoute } from '@angular/router';
@Component({
  selector: 'app-create-or-update-product-specification',
  templateUrl: './create-or-update-product-specification.component.html',
  styleUrls: ['./create-or-update-product-specification.component.scss'],
  standalone: true,
  imports: [
    GridComponent,
    SelectboxComponent,
    TranslateModule,
    BtnSubmitComponent,
    ReactiveFormsModule,
    CommonModule,
  ],
})
export class CreateOrUpdateProductSpecificationComponent
  implements OnInit, OnDestroy
{
  data: BehaviorSubject<any> = new BehaviorSubject<any>(null);
  specificationAttributeList: SpecificationAttribute[] = [];
  specificationAttributeOptionList: SpecificationAttributeOption[] = [];
  form!: FormGroup;
  submitted: boolean = false;
  onDestroy: Subject<void> = new Subject<void>();
  constructor(
    private activatedRoute: ActivatedRoute,
    private productStore: ProductStore,
    private productSpecificationService: ProductSpecificationService,
    private productSpecificationStore: ProductSpecificationStore,
    private specificationAttributeService: SpecificationAttributeService,
    private specificationAttributeOptionService: SpecificationAttributeOptionService,
    private specificationAttributeStore: SpecificationAttributeStore,
    private formBuilder: FormBuilder
  ) {}
  ngOnDestroy(): void {
    this.onDestroy.next();
    this.onDestroy.complete();
  }
  async ngOnInit() {
    this.initForm();
    await this.specificationAttributeService.getAllSpecificationAttribute();
    this.specificationAttributeList =
      this.specificationAttributeStore.specificationAttributeList();

    this.productSpecificationStore.productSpeficationObservable$
      .pipe(takeUntil(this.onDestroy))
      .subscribe((x) => {
        if (x) {
          this.data.next(x);
        }
      });
  }
  initForm() {
    this.form = this.formBuilder.group({
      specificationAttributeOptionId: ['', Validators.required],
      specificationAttributeId: ['', Validators.required],
    });
  }
  hasError(controlName: string, errorName: string) {
    const control = this.form.get(controlName);
    return this.submitted && control?.hasError(errorName);
  }
  async specificationAttributeChange(id: any) {
    await this.specificationAttributeOptionService.getAllSpecificationAttributeOptionBySpecId(
      id
    );
    this.specificationAttributeOptionList =
      this.specificationAttributeStore.specificationAttributeOptionList();
  }

  async createProductSpecification() {
    this.submitted = true;
    if (this.form.valid) {
      let productSpecification = new ProductSpecification();
      productSpecification.languageCode =
        this.productStore.product$().languageCode;
      productSpecification.productId = this.productStore.product$().id;
      productSpecification.specificationAttributeOptionId = this.form.get(
        'specificationAttributeOptionId'
      )?.value;
      await this.productSpecificationService.createproductSpecification(
        productSpecification
      );
      this.form.reset();
      this.form.markAllAsTouched();
      this.submitted = false;
    }
  }
  async getAllData(data?: GridPostData) {
    this.activatedRoute.paramMap.subscribe(async (params) => {
      if (params.get('id')) {
        await this.productSpecificationService.getProductSpecificationGrid({
          ...data,
          productId: params.get('id')!,
        });
      }
    });
  }
}
