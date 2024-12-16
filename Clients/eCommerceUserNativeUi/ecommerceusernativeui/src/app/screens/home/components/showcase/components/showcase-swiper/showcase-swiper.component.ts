import { CommonModule } from '@angular/common';
import {
  AfterViewInit,
  CUSTOM_ELEMENTS_SCHEMA,
  ChangeDetectionStrategy,
  Component,
  ElementRef,
  Input,
  OnInit,
  ViewChild,
  input,
} from '@angular/core';
import { RouterModule } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';
import { LazyImgDirective } from 'src/app/directives/lazy-img.directive';
import { AllShowcaseDTO } from 'src/app/models/responseModel/allShowcaseDTO';
import { GlobalService } from 'src/app/services/core/global.service';
import { FavoriteProductButtonComponent } from 'src/app/uı/favorite-product-button/favorite-product-button.component';
import { StarRatingComponent } from 'src/app/uı/star-rating/star-rating.component';
import { Swiper } from 'swiper/types';
import { NavController } from '@ionic/angular/standalone';
@Component({
  selector: 'app-showcase-swiper',
  templateUrl: './showcase-swiper.component.html',
  styleUrls: ['./showcase-swiper.component.scss'],
  standalone: true,
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  imports: [
    CommonModule,
    LazyImgDirective,
    TranslateModule,
    RouterModule,
    StarRatingComponent,
    FavoriteProductButtonComponent,
  ],
})
export class ShowcaseSwiperComponent implements AfterViewInit {
  @ViewChild('swiper', { static: true }) swiperRef!: ElementRef;
  swiper?: Swiper;
  @Input() data!: AllShowcaseDTO;
  constructor(public glb: GlobalService, public navController: NavController) {}
  goNext() {
    this.swiper?.slideNext();
  }
  goPrev() {
    this.swiper?.slidePrev();
  }
  ngAfterViewInit(): void {
    this.swiper = this.swiperRef?.nativeElement.swiper;
  }
}
