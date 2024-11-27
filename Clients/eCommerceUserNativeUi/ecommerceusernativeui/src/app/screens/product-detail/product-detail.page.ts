import { Component } from '@angular/core';
import {
  IonHeader,
  IonToolbar,
  IonTitle,
  IonContent,
  IonRouterOutlet,
} from '@ionic/angular/standalone';
import { OnInit } from '@angular/core';
import { GlobalService } from 'src/app/services/global.service';
import { FooterComponent } from 'src/app/uı/footer/footer.component';
import { HeaderComponent } from 'src/app/uı/header/header.component';
import { MobileFooterComponent } from 'src/app/uı/mobile-footer/mobile-footer.component';
@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.page.html',
  styleUrls: ['./product-detail.page.scss'],
  standalone: true,
  imports: [
    IonContent,
    HeaderComponent,
    FooterComponent,
    MobileFooterComponent,
  ],
})
export class ProductDetailPage implements OnInit {
  constructor(public glb: GlobalService) {}

  ngOnInit() {}
}
