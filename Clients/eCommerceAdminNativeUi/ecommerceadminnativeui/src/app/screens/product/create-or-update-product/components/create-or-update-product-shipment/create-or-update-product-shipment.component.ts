import { Component, OnInit, ViewChild } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Subject } from 'rxjs';
import { ProductShipmentInfoService } from './../../../../../services/product/product-shipment-info.service';
import { ProductShipmentInfoStore } from 'src/app/stores/product-shıpment-info';
import { ProductStore } from 'src/app/stores/product.store';
import { BtnSubmitComponent } from '../../../../../u\u0131/btn-submit/btn-submit.component';
import { TranslateModule } from '@ngx-translate/core';
import { InputComponent } from '../../../../../u\u0131/input/input.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-create-or-update-product-shipment',
  templateUrl: './create-or-update-product-shipment.component.html',
  styleUrls: ['./create-or-update-product-shipment.component.scss'],
  standalone: true,
  imports: [
    BtnSubmitComponent,
    TranslateModule,
    ReactiveFormsModule,
    InputComponent,
    CommonModule,
  ],
})
export class CreateOrUpdateProductShipmentComponent implements OnInit {
  form!: FormGroup;
  submitted: boolean = false;
  onDestroy: Subject<void> = new Subject<void>();
  isCreate: boolean = true;
  constructor(
    private activatedRoute: ActivatedRoute,
    private productShipmentInfoService: ProductShipmentInfoService,
    private productShipmentInfoStore: ProductShipmentInfoStore,
    private productStore: ProductStore,
    private formBuilder: FormBuilder
  ) {}

  ngOnDestroy(): void {
    this.onDestroy.next();
    this.onDestroy.complete();
  }
  ngOnInit() {
    this.initForm();
    this.activatedRoute.paramMap.subscribe(async (params) => {
      if (params.get('id')) {
        await this.productShipmentInfoService
          .getProductShipmentInfoByProductId(params.get('id')!)
          .then((x) => {
            if (x) {
              this.isCreate = false;
            }
          });
        this.form.patchValue(
          this.productShipmentInfoStore.productShipmentInfo$()
        );
      }
    });
  }
  initForm() {
    this.form = this.formBuilder.group({
      id: [''],
      width: [0, Validators.required],
      length: [0, Validators.required],
      height: [0, Validators.required],
      weight: [0, Validators.required],
    });
  }
  hasError(controlName: string, errorName: string) {
    const control = this.form.get(controlName);
    return this.submitted && control?.hasError(errorName);
  }
  async createOrUpdateProductShipmentInfo() {
    this.submitted = true;
    if (this.form.valid) {
      var data = {
        ...this.form.value,
        productId: this.productStore.product$().id,
        languageCode: this.productStore.product$().languageCode,
      };
      if (this.isCreate) {
        this.isCreate = false;
        await this.productShipmentInfoService
          .createproductShipmentInfo(data)
          .then((x) => {
            this.form.patchValue(
              this.productShipmentInfoStore.productShipmentInfo$()
            );
          });
      } else {
        this.productShipmentInfoService.updateProductShipmentInfo(data);
      }
      this.submitted = false;
    }
  }
}
