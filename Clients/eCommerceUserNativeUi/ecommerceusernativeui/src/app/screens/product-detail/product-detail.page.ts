import { Component, signal, Signal, WritableSignal } from '@angular/core';
import { IonContent, NavController } from '@ionic/angular/standalone';
import { OnInit } from '@angular/core';
import { GlobalService } from 'src/app/services/core/global.service';
import { FooterComponent } from 'src/app/uı/footer/footer.component';
import { HeaderComponent } from 'src/app/uı/header/header.component';
import { MobileFooterComponent } from 'src/app/uı/mobile-footer/mobile-footer.component';
import { ProductStore } from 'src/app/stores/product.store';
import { ProductService } from 'src/app/services/product.service';
import { ProductDto } from 'src/app/models/responseModel/productDto';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ProductTabsComponent } from './components/product-tabs/product-tabs.component';
import { ProductSpecificationService } from 'src/app/services/product-specification.service';
import { GridPostData } from 'src/app/models/core/grid';

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
    CommonModule,
    ProductTabsComponent,
  ],
})
export class ProductDetailPage implements OnInit {
  productDto$: WritableSignal<ProductDto> = signal(new ProductDto());

  constructor(
    public glb: GlobalService,
    private productService: ProductService,
    public productStore: ProductStore,
    private activatedRoute: ActivatedRoute,
    private navController: NavController,
    private productSpecificationService: ProductSpecificationService
  ) {}

  ngOnInit() {
    if (this.productStore.selectedProductId$()) {
      this.getProductDtoById();
    } else {
      this.getProductDtoBySlug();
    }
  }
  getProductDtoBySlug() {
    this.activatedRoute.paramMap.subscribe(async (params) => {
      const slug = params.get('slug');
      if (slug) {
        this.productService.getProductDtoBySlug(slug).then((x) => {
          this.productDto$.set(this.productStore.productDTO$());
        });
      } else {
        this.navController.navigateForward('');
      }
    });
  }
  getProductDtoById() {
    this.productService
      .getProductDtoById(this.productStore.selectedProductId$())
      .then((x) => {
        this.productDto$.set(this.productStore.productDTO$());
      });
  }
}
