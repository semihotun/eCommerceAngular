import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { IonContent, NavController } from '@ionic/angular/standalone';
import { SubHeaderComponent } from '../../u\u0131/sub-header/sub-header.component';
import { HeaderComponent } from '../../u\u0131/header/header.component';
import { FooterComponent } from '../../u\u0131/footer/footer.component';
import { MobileFooterComponent } from '../../u\u0131/mobile-footer/mobile-footer.component';
import { TranslateModule } from '@ngx-translate/core';
import { GlobalService } from 'src/app/services/core/global.service';
import { CustomerUserAddressStore } from './../../stores/customer-user-address.store';
import { CustomerUserAddressService } from 'src/app/services/customer-user-address.service';
@Component({
  selector: 'app-addresses',
  templateUrl: './addresses.page.html',
  styleUrls: ['./addresses.page.scss'],
  standalone: true,
  imports: [
    IonContent,
    TranslateModule,
    CommonModule,
    FormsModule,
    SubHeaderComponent,
    HeaderComponent,
    FooterComponent,
    MobileFooterComponent,
  ],
})
export class AddressesPage {
  constructor(
    public glb: GlobalService,
    public navController: NavController,
    private customerUserAddressService: CustomerUserAddressService,
    public customerUserAddressStore: CustomerUserAddressStore
  ) {}
  ionViewWillEnter() {
    this.customerUserAddressService.getAllCustomerUserAdressDto();
  }
  deleteClick(id: string) {
    this.customerUserAddressService.deleteCustomerUserAddress(id);
  }
}
