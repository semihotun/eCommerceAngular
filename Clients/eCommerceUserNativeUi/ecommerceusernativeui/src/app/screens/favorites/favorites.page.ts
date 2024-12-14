import { ProductFavoriteStore } from './../../stores/product-favorite.store';
import { ProductFavoriteService } from './../../services/product-favorite.service';
import { Component, OnInit } from '@angular/core';
import {
  IonContent,
  InfiniteScrollCustomEvent,
  IonInfiniteScroll,
  IonInfiniteScrollContent,
} from '@ionic/angular/standalone';
import { HeaderComponent } from '../../u\u0131/header/header.component';
import { SubHeaderComponent } from '../../u\u0131/sub-header/sub-header.component';
import { TranslateModule } from '@ngx-translate/core';
import { MobileFooterComponent } from '../../u\u0131/mobile-footer/mobile-footer.component';
import { FooterComponent } from '../../u\u0131/footer/footer.component';
import { GlobalService } from 'src/app/services/core/global.service';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FavoriteProductButtonComponent } from '../../u\u0131/favorite-product-button/favorite-product-button.component';
import { StarRatingComponent } from '../../u\u0131/star-rating/star-rating.component';

@Component({
  selector: 'app-favorites',
  templateUrl: './favorites.page.html',
  styleUrls: ['./favorites.page.scss'],
  standalone: true,
  imports: [
    IonInfiniteScrollContent,
    IonInfiniteScroll,
    TranslateModule,
    SubHeaderComponent,
    HeaderComponent,
    IonContent,
    MobileFooterComponent,
    FooterComponent,
    CommonModule,
    RouterModule,
    FavoriteProductButtonComponent,
    StarRatingComponent,
  ],
})
export class FavoritesPage implements OnInit {
  pageIndex: number = 1;
  pageSıze: number = 7;
  constructor(
    public glb: GlobalService,
    private productFavoriteService: ProductFavoriteService,
    public productFavoriteStore: ProductFavoriteStore
  ) {}
  ionViewWillEnter() {
    this.getFavorites();
  }
  getFavorites() {
    this.productFavoriteService
      .getAllFavoriteProduct(this.pageIndex, this.pageSıze)
      .then(() => {
        if (
          this.productFavoriteStore.productFavoriteList$().pageIndex >
          this.productFavoriteStore.productFavoriteList$().totalPages
        ) {
          this.pageIndex =
            this.productFavoriteStore.productFavoriteList$().totalPages;
        }
      });
  }
  ngOnInit() {
    this.getFavorites();
  }
  deleteFavorite(id: string) {
    this.ionViewWillEnter();
  }
  onIonInfinite(ev: InfiniteScrollCustomEvent) {
    setTimeout(() => {
      this.pageIndex++;
      this.getFavorites();
      ev.target.complete();
    }, 1000);
  }
}
