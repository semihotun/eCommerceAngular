import { CommonModule } from '@angular/common';
import { Component, OnInit, signal, WritableSignal } from '@angular/core';
import { HeaderComponent } from 'src/app/uı/header/header.component';
import { StarRatingComponent } from 'src/app/uı/star-rating/star-rating.component';
import { NavController } from '@ionic/angular/standalone';
import { ProductDto } from 'src/app/models/responseModel/productDto';
import { GlobalService } from 'src/app/services/core/global.service';
import { ProductService } from 'src/app/services/product.service';
import { ProductStore } from 'src/app/stores/product.store';
import { ActivatedRoute } from '@angular/router';
import { MobileProductTabsComponent } from './components/mobile-product-tabs/mobile-product-tabs.component';
import { TranslateModule } from '@ngx-translate/core';
import { FavoriteProductButtonComponent } from '../../../../u\u0131/favorite-product-button/favorite-product-button.component';
@Component({
  selector: 'app-mobile-product-detail',
  templateUrl: './mobile-product-detail.component.html',
  styleUrls: ['./mobile-product-detail.component.scss'],
  standalone: true,
  imports: [
    HeaderComponent,
    TranslateModule,
    CommonModule,
    StarRatingComponent,
    MobileProductTabsComponent,
    FavoriteProductButtonComponent,
  ],
})
export class MobileProductDetailComponent implements OnInit {
  productDto$: WritableSignal<ProductDto> = signal(new ProductDto());

  constructor(
    private productService: ProductService,
    public productStore: ProductStore,
    private activatedRoute: ActivatedRoute,
    private navController: NavController
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
          console.log(this.productStore.productDTO$());
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
        console.log(this.productStore.productDTO$());
      });
  }
}
