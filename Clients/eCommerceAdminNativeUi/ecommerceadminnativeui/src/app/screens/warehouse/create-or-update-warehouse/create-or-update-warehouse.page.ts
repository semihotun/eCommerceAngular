import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { HeaderComponent } from 'src/app/uı/header/header.component';
import { BtnSubmitComponent } from 'src/app/uı/btn-submit/btn-submit.component';
import { InputComponent } from 'src/app/uı/input/input.component';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { ActivatedRoute } from '@angular/router';
import { WarehouseService } from 'src/app/services/warehouse.service';
import { WarehouseStore } from 'src/app/stores/warehouse.store';
import { NavController } from '@ionic/angular/standalone';
@Component({
  selector: 'app-create-or-update-warehouse',
  templateUrl: './create-or-update-warehouse.page.html',
  styleUrls: ['./create-or-update-warehouse.page.scss'],
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    HeaderComponent,
    BtnSubmitComponent,
    InputComponent,
    TranslateModule,
  ],
})
export class CreateOrUpdateWarehousePage implements OnInit {
  title: 'Update Warehouse' | 'Create Warehouse' = 'Create Warehouse';
  form!: FormGroup;
  submitted: boolean = false;
  isCreate: boolean = true;
  constructor(
    private formBuilder: FormBuilder,
    private activatedRoute: ActivatedRoute,
    private warehouseService: WarehouseService,
    private warehouseStore: WarehouseStore,
    private navCtrl: NavController,
    private translateService: TranslateService
  ) {}
  initForm() {
    this.form = this.formBuilder.group({
      id: [''],
      name: ['', Validators.required],
      address: ['', Validators.required],
    });
  }
  async ngOnInit() {
    this.initForm();
    this.activatedRoute.paramMap.subscribe(async (params) => {
      if (params.get('id')) {
        console.log('act', this.translateService.currentLang);
        await this.warehouseService.getWarehouseById(params.get('id')!);
        this.form.patchValue(this.warehouseStore.warehouse$());
        this.isCreate = false;
        this.title = 'Update Warehouse';
      } else {
        this.title = 'Create Warehouse';
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
        await this.warehouseService.createwarehouse(this.form.value);
      } else {
        await this.warehouseService.updateWarehouse(this.form.value);
      }
      this.navCtrl.navigateForward('/warehouse-list');
    }
  }
}
