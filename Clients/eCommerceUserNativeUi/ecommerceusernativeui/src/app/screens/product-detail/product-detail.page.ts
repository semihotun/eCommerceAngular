import { Component } from '@angular/core';
import { OnInit } from '@angular/core';
import { GlobalService } from 'src/app/services/core/global.service';
import { CommonModule } from '@angular/common';
import { WebProductDetailComponent } from './components/web-product-detail/web-product-detail.component';
import { MobileProductDetailComponent } from './components/mobile-product-detail/mobile-product-detail.component';
import { IonContent } from '@ionic/angular/standalone';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.page.html',
  styleUrls: ['./product-detail.page.scss'],
  standalone: true,
  imports: [
    IonContent,
    CommonModule,
    WebProductDetailComponent,
    MobileProductDetailComponent,
    IonContent,
  ],
})
export class ProductDetailPage implements OnInit {
  constructor(public glb: GlobalService) {}
  ngOnInit(): void {}
}
