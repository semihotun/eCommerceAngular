import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { BtnSubmitComponent } from '../../../../../u\u0131/btn-submit/btn-submit.component';
import { TranslateModule } from '@ngx-translate/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { BehaviorSubject, Subject, takeUntil } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { ProductPhotoStore } from 'src/app/stores/product-photo.store';
import { ProductStore } from 'src/app/stores/product.store';
import { ProductPhotoService } from 'src/app/services/product/product-photo.service';
import { ProductPhoto } from 'src/app/models/responseModel/productPhoto';
import { GridPostData } from 'src/app/models/core/grid';
import { GridComponent } from 'src/app/uı/grid/grid.component';
import { ImagePickerComponent } from '../../../../../u\u0131/image-picker/image-picker.component';

@Component({
  selector: 'app-create-or-update-product-photo',
  templateUrl: './create-or-update-product-photo.component.html',
  styleUrls: ['./create-or-update-product-photo.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
    BtnSubmitComponent,
    TranslateModule,
    ReactiveFormsModule,
    GridComponent,
    ImagePickerComponent,
  ],
})
export class CreateOrUpdateProductPhotoComponent implements OnInit, OnDestroy {
  form!: FormGroup;
  submitted: boolean = false;
  onDestroy: Subject<void> = new Subject<void>();
  data: BehaviorSubject<any> = new BehaviorSubject<any>(null);
  @ViewChild('productPhotoGrid') productPhotoGrid!: GridComponent;

  constructor(
    private activatedRoute: ActivatedRoute,
    private productStore: ProductStore,
    private productPhotoService: ProductPhotoService,
    private productPhotoStore: ProductPhotoStore,
    private formBuilder: FormBuilder
  ) {}

  ngOnDestroy(): void {
    this.onDestroy.next();
    this.onDestroy.complete();
  }
  ngOnInit() {
    this.initForm();
    this.productPhotoStore.productPhotoByProductIdListObservable$
      .pipe(takeUntil(this.onDestroy))
      .subscribe((x) => {
        if (x) {
          this.data.next(x);
        }
      });
  }
  initForm() {
    this.form = this.formBuilder.group({
      photoBase64: ['', Validators.required],
    });
  }
  hasError(controlName: string, errorName: string) {
    const control = this.form.get(controlName);
    return this.submitted && control?.hasError(errorName);
  }
  async createProductPhoto() {
    this.submitted = true;
    if (this.form.valid) {
      let productPhoto = new ProductPhoto();
      productPhoto.languageCode = this.productStore.product$().languageCode;
      productPhoto.productId = this.productStore.product$().id;
      productPhoto.photoBase64 = this.form.get('photoBase64')?.value;
      await this.productPhotoService.createProductPhoto(productPhoto);
      this.productPhotoGrid.refresh();
      this.form.reset();
      this.form.markAllAsTouched();
      this.submitted = false;
    }
  }
  async getAllData(data?: GridPostData) {
    this.activatedRoute.paramMap.subscribe(async (params) => {
      if (params.get('id')) {
        await this.productPhotoService.getProductPhotoByIdGrid({
          ...data,
          productId: params.get('id')!,
        });
      }
    });
  }
}
