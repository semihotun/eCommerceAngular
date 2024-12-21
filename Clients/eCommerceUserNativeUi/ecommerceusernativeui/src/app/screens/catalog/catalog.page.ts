import {
  Component,
  ElementRef,
  OnDestroy,
  OnInit,
  ViewChild,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import {
  IonContent,
  IonInfiniteScroll,
  IonInfiniteScrollContent,
  NavController,
  InfiniteScrollCustomEvent,
} from '@ionic/angular/standalone';
import { TranslateModule } from '@ngx-translate/core';
import { FooterComponent } from 'src/app/uı/footer/footer.component';
import { MobileFooterComponent } from 'src/app/uı/mobile-footer/mobile-footer.component';
import { HeaderComponent } from 'src/app/uı/header/header.component';
import { SubHeaderComponent } from 'src/app/uı/sub-header/sub-header.component';
import { FavoriteProductButtonComponent } from 'src/app/uı/favorite-product-button/favorite-product-button.component';
import { StarRatingComponent } from 'src/app/uı/star-rating/star-rating.component';
import { GlobalService } from 'src/app/services/core/global.service';
import { ActivatedRoute } from '@angular/router';
import { CatalogService } from 'src/app/services/catalog.service';
import { GetCatalogProductDTOBySlugRequest } from 'src/app/models/requestModel/getCatalogProductDTO';
import { CatalogStore } from './../../stores/catalog.store';
import { CatalogFilterComponent } from './components/catalog-filter/catalog-filter.component';
import { Subject } from 'rxjs';
import { PagedList } from 'src/app/models/core/grid';
import { GetCatalogProductDTO } from 'src/app/models/responseModel/getCatalogProductDTO';

@Component({
  selector: 'app-catalog',
  templateUrl: './catalog.page.html',
  styleUrls: ['./catalog.page.scss'],
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
    CatalogFilterComponent,
  ],
})
export class CatalogPage implements OnInit, OnDestroy {
  @ViewChild('filterWrapper') filterWrapper!: ElementRef;
  isFilterFixed: boolean = false;
  pageIndex: number = 1;
  pageSize: number = 20;
  categoryId!: string;
  categorySlug!: string;
  filterWrapperTop!: number;
  onDestroy = new Subject<void>();
  @ViewChild('catalogFilterComponent')
  catalogFilterComponent!: CatalogFilterComponent;
  constructor(
    public glb: GlobalService,
    private activatedRoute: ActivatedRoute,
    private navController: NavController,
    private catalogService: CatalogService,
    public catalogStore: CatalogStore
  ) {}

  ngOnDestroy(): void {
    this.onDestroy.next();
    this.onDestroy.complete();
  }
  ngOnInit(): void {}
  ionViewWillEnter() {
    this.filterWrapperTop = this.filterWrapper.nativeElement.offsetTop;
    this.activatedRoute.paramMap.subscribe(async (params) => {
      this.categorySlug = params.get('slug')!;
      if (this.categorySlug) {
        this.getCatalogProductDtoBySlug();
        this.catalogService.getCatalogDTOBySlug(this.categorySlug);
      } else {
        this.navController.navigateForward('');
      }
    });
  }
  resetAndgetCatalogProductDtoBySlug() {
    this.catalogStore.setCatalogProductDto(
      new PagedList<GetCatalogProductDTO>()
    );
    this.getCatalogProductDtoBySlug();
  }
  getCatalogProductDtoBySlug() {
    let request = new GetCatalogProductDTOBySlugRequest();
    request.categorySlug = this.categorySlug;
    request.maxPrice = this.catalogStore.maxPrice$();
    request.minPrice = this.catalogStore.minPrice$();
    request.pageIndex = this.pageIndex;
    request.pageSize = this.pageSize;
    request.specifications = this.catalogStore.filterList$();
    this.catalogService.getCatalogProductDtoBySlug(request);
  }
  ionViewWillLeave() {
    this.catalogStore.resetState();
  }
  onIonInfinite(ev: InfiniteScrollCustomEvent) {
    setTimeout(() => {
      this.pageIndex++;
      this.getCatalogProductDtoBySlug();
      ev.target.complete();
    }, 1000);
  }
  openFilterMenu() {
    this.catalogFilterComponent.openFilterMenu();
  }
  removeFilter(e: Event, specOptionId: string) {
    e.stopPropagation();
    let data = this.catalogStore
      .filterList$()
      .filter((x) => x.specificationAttributeOptionId != specOptionId);
    this.catalogStore.setFilterList(data);
    this.catalogStore.setRemovedOptionId(specOptionId);
    this.resetAndgetCatalogProductDtoBySlug();
  }
  clearMinPrice(e: Event) {
    e.stopPropagation();
    this.catalogStore.setMinPrice(0);
    this.resetAndgetCatalogProductDtoBySlug();
  }
  clearMaxPrice(e: Event) {
    e.stopPropagation();
    this.catalogStore.setMaxPrice(0);
    this.resetAndgetCatalogProductDtoBySlug();
  }
  onScroll(event: any) {
    const scrollTop = event.detail.scrollTop;
    if (scrollTop > this.filterWrapperTop) {
      this.isFilterFixed = true;
    } else {
      this.isFilterFixed = false;
    }
  }
}
