import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import {
  IonContent,
  IonInfiniteScroll,
  IonInfiniteScrollContent,
  NavController,
} from '@ionic/angular/standalone';
import { FooterComponent } from '../../u\u0131/footer/footer.component';
import { MobileFooterComponent } from '../../u\u0131/mobile-footer/mobile-footer.component';
import { HeaderComponent } from '../../u\u0131/header/header.component';
import { SubHeaderComponent } from '../../u\u0131/sub-header/sub-header.component';
import { FavoriteProductButtonComponent } from '../../u\u0131/favorite-product-button/favorite-product-button.component';
import { StarRatingComponent } from '../../u\u0131/star-rating/star-rating.component';
import { GlobalService } from 'src/app/services/core/global.service';
import { TranslateModule } from '@ngx-translate/core';
import { ShowcaseStore } from 'src/app/stores/shocase.store';
import { ShowcaseService } from 'src/app/services/showcase.service';
import { InfiniteScrollCustomEvent } from '@ionic/core';
import { ActivatedRoute } from '@angular/router';
import { PagedList } from 'src/app/models/core/grid';
import { GetHomeShowcaseDetailDTO } from 'src/app/models/responseModel/getHomeShowcaseDetailDTO';

@Component({
  selector: 'app-showcase-detail',
  templateUrl: './showcase-detail.page.html',
  styleUrls: ['./showcase-detail.page.scss'],
  standalone: true,
  imports: [
    IonInfiniteScrollContent,
    IonInfiniteScroll,
    IonContent,
    TranslateModule,
    CommonModule,
    FormsModule,
    FooterComponent,
    MobileFooterComponent,
    HeaderComponent,
    SubHeaderComponent,
    FavoriteProductButtonComponent,
    StarRatingComponent,
  ],
})
export class ShowcaseDetailPage {
  pageIndex: number = 1;
  pageSize: number = 10;
  showcaseId!: string;
  constructor(
    public glb: GlobalService,
    public showcaseStore: ShowcaseStore,
    private showcaseService: ShowcaseService,
    private activatedRoute: ActivatedRoute,
    private navController: NavController
  ) {}
  ionViewWillEnter() {
    this.activatedRoute.paramMap.subscribe(async (params) => {
      this.showcaseId = params.get('id')!;
      if (this.showcaseId) {
        this.getShowcaseProduct();
      } else {
        this.navController.navigateForward('');
      }
    });
  }
  ionViewWillLeave() {
    this.showcaseStore.setShowcaseList(
      new PagedList<GetHomeShowcaseDetailDTO>()
    );
  }
  getShowcaseProduct() {
    this.showcaseService.getHomeShowcaseDetail({
      pageIndex: this.pageIndex,
      pageSize: this.pageSize,
      showCaseId: this.showcaseId,
    });
  }
  onIonInfinite(ev: InfiniteScrollCustomEvent) {
    setTimeout(() => {
      this.pageIndex++;
      this.getShowcaseProduct();
      ev.target.complete();
    }, 1000);
  }
}
