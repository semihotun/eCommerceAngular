import { CommonModule } from '@angular/common';
import { Component, OnInit, signal, WritableSignal } from '@angular/core';
import { HeaderComponent } from 'src/app/uı/header/header.component';
import { StarRatingComponent } from 'src/app/uı/star-rating/star-rating.component';
import { ProductStore } from 'src/app/stores/product.store';
import { MobileProductTabsComponent } from './components/mobile-product-tabs/mobile-product-tabs.component';
import { TranslateModule } from '@ngx-translate/core';
import { FavoriteProductButtonComponent } from '../../../../u\u0131/favorite-product-button/favorite-product-button.component';
import { GlobalService } from 'src/app/services/core/global.service';
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
  constructor(public productStore: ProductStore, public glb: GlobalService) {}
  ngOnInit() {}
}
