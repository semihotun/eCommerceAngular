import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ProductFavoriteService } from './../../services/product-favorite.service';
import { CustomerUserService } from 'src/app/services/customer-user.service';
import { NavController } from '@ionic/angular/standalone';
import { ToastService } from './../../services/core/toast.service';
import { HomeStore } from 'src/app/stores/home.store';

@Component({
  selector: 'app-favorite-product-button',
  templateUrl: './favorite-product-button.component.html',
  styleUrls: ['./favorite-product-button.component.scss'],
  standalone: true,
})
export class FavoriteProductButtonComponent implements OnInit {
  @Input() size: number = 5;
  @Input() productId!: string;
  @Input() favoriteId!: string | null;
  @Output() deleteClick = new EventEmitter<any>();
  @Output() addClick = new EventEmitter<any>();
  constructor(
    private productFavoriteService: ProductFavoriteService,
    private customerUserService: CustomerUserService,
    private navController: NavController,
    private toastService: ToastService,
    private homeStore: HomeStore
  ) {}
  ngOnInit() {}

  isLogin(): boolean {
    if (!this.customerUserService.isLogin()) {
      this.navController.navigateForward('login');
      this.toastService.presentDangerToast('Please Login', 5000);
      return false;
    }
    return true;
  }
  addFavorite(event: MouseEvent) {
    event.stopPropagation();
    if (this.productId && this.isLogin()) {
      this.productFavoriteService
        .createProductFavorite({ productId: this.productId })
        .then((x: string) => {
          this.favoriteId = x;
          this.updateHomeShowcaseProductFavorite(
            this.productId,
            this.favoriteId
          );
          this.addClick.emit(this.favoriteId);
        });
    }
  }
  removeFavorite(event: MouseEvent) {
    event.stopPropagation();
    if (this.favoriteId != null && this.isLogin()) {
      this.productFavoriteService
        .deleteProductFavorite(this.favoriteId)
        .then((x) => {
          this.favoriteId = null;
          this.updateHomeShowcaseProductFavorite(
            this.productId,
            this.favoriteId
          );
          this.deleteClick.emit(this.favoriteId);
        });
    }
  }

  updateHomeShowcaseProductFavorite(
    productId: string,
    favoriteId: string | null
  ) {
    const updatedShowcases = this.homeStore.showcases$().map((showcase) => {
      showcase.showCaseProductList = showcase.showCaseProductList.map(
        (product) => {
          if (product.id === productId) {
            return { ...product, favoriteId: favoriteId };
          }
          return product;
        }
      );
      return showcase;
    });
    this.homeStore.setShowcases(updatedShowcases);
  }
}
