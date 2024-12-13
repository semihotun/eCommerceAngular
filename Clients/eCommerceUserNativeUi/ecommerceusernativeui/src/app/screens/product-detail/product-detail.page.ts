import { Component } from '@angular/core';
import { OnInit } from '@angular/core';
import { GlobalService } from 'src/app/services/core/global.service';
import { CommonModule } from '@angular/common';
import { WebProductDetailComponent } from './components/web-product-detail/web-product-detail.component';
import { MobileProductDetailComponent } from './components/mobile-product-detail/mobile-product-detail.component';
import { IonContent, NavController } from '@ionic/angular/standalone';
import { ProductService } from 'src/app/services/product.service';
import { ProductStore } from 'src/app/stores/product.store';
import { ActivatedRoute } from '@angular/router';

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
  constructor(
    private productService: ProductService,
    public productStore: ProductStore,
    private activatedRoute: ActivatedRoute,
    private navController: NavController,
    public glb: GlobalService
  ) {}
  ngOnInit() {
    if (this.productStore.selectedProductId$() != '') {
      this.getProductDtoById();
    } else {
      this.getProductDtoBySlug();
    }
  }
  ionViewWillLeave() {
    this.productStore.setSelectedProductId('');
  }
  getProductDtoBySlug() {
    this.activatedRoute.paramMap.subscribe(async (params) => {
      const slug = params.get('slug');
      if (slug) {
        this.productService.getProductDtoBySlug(slug);
      } else {
        this.navController.navigateForward('');
      }
    });
  }
  getProductDtoById() {
    this.productService.getProductDtoById(
      this.productStore.selectedProductId$()
    );
  }
}
