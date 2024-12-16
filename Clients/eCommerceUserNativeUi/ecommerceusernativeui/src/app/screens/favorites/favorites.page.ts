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
import { PagedList } from 'src/app/models/core/grid';

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
export class FavoritesPage {
  pageIndex: number = 1;
  pageSize: number = 7;
  deletedCount: number = 0;
  totalPages: number = 0;
  removeableCount: number = 2;
  constructor(
    public glb: GlobalService,
    private productFavoriteService: ProductFavoriteService,
    public productFavoriteStore: ProductFavoriteStore
  ) {}
  ionViewWillEnter() {
    this.deletedCount = 0;
    this.getFavorites();
  }
  getFavorites() {
    this.productFavoriteService
      .getAllFavoriteProduct(
        this.pageIndex,
        this.pageSize,
        this.pageSize - this.deletedCount
      )
      .then((x) => {
        this.totalPages =
          this.productFavoriteStore.productFavoriteList$().totalPages;
        this.deletedCount = 0;
      });
  }
  deleteFavorite(id: string) {
    this.productFavoriteStore.deleteProductFavoriteById(id);
    this.deletedCount++;
    if (this.deletedCount == this.removeableCount) {
      this.getFavorites();
      this.deletedCount = 0;
    }
  }
  onIonInfinite(ev: InfiniteScrollCustomEvent) {
    setTimeout(() => {
      if (this.deletedCount == 0) {
        this.pageIndex++;
      }
      this.getFavorites();
      ev.target.complete();
    }, 1000);
  }
  ionViewWillLeave() {
    this.productFavoriteStore.resetProductFavoriteList();
  }
}
