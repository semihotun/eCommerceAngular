import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FooterComponent } from 'src/app/uı/footer/footer.component';
import { HeaderComponent } from 'src/app/uı/header/header.component';
import { StarRatingComponent } from 'src/app/uı/star-rating/star-rating.component';
import { ProductStore } from 'src/app/stores/product.store';
import { WebProductTabsComponent } from './components/web-product-tabs/web-product-tabs.component';
import { FavoriteProductButtonComponent } from '../../../../u\u0131/favorite-product-button/favorite-product-button.component';
@Component({
  selector: 'app-web-product-detail',
  templateUrl: './web-product-detail.component.html',
  styleUrls: ['./web-product-detail.component.scss'],
  standalone: true,
  imports: [
    HeaderComponent,
    FooterComponent,
    CommonModule,
    StarRatingComponent,
    WebProductTabsComponent,
    FavoriteProductButtonComponent,
  ],
})
export class WebProductDetailComponent implements OnInit {
  constructor(public productStore: ProductStore) {}
  ngOnInit() {}
}
