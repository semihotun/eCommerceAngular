import { Component, inject, OnInit } from '@angular/core';
import { IonContent, NavController } from '@ionic/angular/standalone';
import { CityDistrictService } from './../../services/city-district.service';
import { CityAndDistrictStore } from './../../stores/city-state.store';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { GlobalService } from 'src/app/services/core/global.service';
import { BtnSubmitComponent } from '../../u\u0131/btn-submit/btn-submit.component';
import { TranslateModule } from '@ngx-translate/core';
import { HeaderComponent } from '../../u\u0131/header/header.component';
import { SubHeaderComponent } from '../../u\u0131/sub-header/sub-header.component';
import { InputComponent } from '../../u\u0131/input/input.component';
import { FooterComponent } from '../../u\u0131/footer/footer.component';
import { MobileFooterComponent } from '../../u\u0131/mobile-footer/mobile-footer.component';
import { CommonModule } from '@angular/common';
import { PaginationSelectboxComponent } from '../../u\u0131/pagination-selectbox/pagination-selectbox.component';
import { TextareaComponent } from '../../u\u0131/textarea/textarea.component';
import { CustomerUserAddressService } from 'src/app/services/customer-user-address.service';
import { CustomerUserAddress } from './../../models/responseModel/customerUserAddress';
@Component({
  selector: 'app-add-address',
  templateUrl: './add-address.page.html',
  styleUrls: ['./add-address.page.scss'],
  standalone: true,
  imports: [
    CommonModule,
    IonContent,
    BtnSubmitComponent,
    TranslateModule,
    HeaderComponent,
    SubHeaderComponent,
    InputComponent,
    FooterComponent,
    MobileFooterComponent,
    ReactiveFormsModule,
    PaginationSelectboxComponent,
    TextareaComponent,
  ],
})
export class AddAddressPage implements OnInit {
  form!: FormGroup;
  glb = inject(GlobalService);
  submitted: boolean = false;
  pageSize: number = 20;
  isSelectedCity: boolean = false;
  constructor(
    public cityAndDistrictStore: CityAndDistrictStore,
    public cityDistrictService: CityDistrictService,
    private formBuilder: FormBuilder,
    private navController: NavController,
    private customerUserAddressService: CustomerUserAddressService
  ) {}
  hasError(controlName: string, errorName: string) {
    const control = this.form.get(controlName);
    return this.submitted && control?.hasError(errorName);
  }
  ngOnInit() {
    this.initForm();
    this.cityDistrictService.getAllCity(1, this.pageSize, '', false);
  }
  async changeCity() {
    await this.cityDistrictService
      .getAllDistrictByCityId(
        1,
        this.pageSize,
        '',
        true,
        this.form.get('cityId')?.value
      )
      .then(() => {
        this.isSelectedCity = true;
      });
  }
  citySearch(eventData: {
    pageIndex: number;
    pageSize: number;
    filterText: string;
    replace: boolean;
  }) {
    this.cityDistrictService.getAllCity(
      eventData.pageIndex,
      eventData.pageSize,
      eventData.filterText,
      eventData.replace
    );
  }
  districtSearch(eventData: {
    pageIndex: number;
    pageSize: number;
    filterText: string;
    replace: boolean;
  }) {
    this.cityDistrictService.getAllDistrictByCityId(
      eventData.pageIndex,
      eventData.pageSize,
      eventData.filterText,
      eventData.replace,
      this.form.get('cityId')?.value
    );
  }
  initForm() {
    this.form = this.formBuilder.group({
      id: [''],
      name: ['', Validators.required],
      cityId: ['', Validators.required],
      districtId: ['', Validators.required],
      street: ['', Validators.required],
      address: ['', Validators.required],
    });
  }
  async saveForm() {
    this.submitted = true;
    if (this.form.valid) {
      await this.customerUserAddressService.createcustomerUserAddress(
        this.form.value
      );
      this.navController.navigateForward('/addresses');
      this.submitted = false;
    }
  }
}
