import { CommonModule } from '@angular/common';
import {
  AfterViewInit,
  CUSTOM_ELEMENTS_SCHEMA,
  Component,
  ElementRef,
  OnInit,
  ViewChild,
  inject,
} from '@angular/core';
import { TranslateModule } from '@ngx-translate/core';
import { HomeService } from 'src/app/services/home.service';
import { HomeStore } from 'src/app/stores/home.store';
import { Swiper } from 'swiper/types';
import { LazyImgDirective } from './../../../../directives/lazy-img.directive';
import { Slider } from 'src/app/models/responseModel/Slider';
import { GlobalService } from 'src/app/services/core/global.service';
@Component({
  selector: 'app-slider',
  templateUrl: './slider.component.html',
  standalone: true,
  styleUrls: ['./slider.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  imports: [CommonModule, TranslateModule, LazyImgDirective],
})
export class SliderComponent implements OnInit {
  constructor(public glb: GlobalService) {}
  @ViewChild('sliderWrapper') sliderWrapper!: ElementRef;
  currentIndex = 0;
  startX = 0;
  endX = 0;
  homeStore = inject(HomeStore);
  autoplayInterval: any;
  ngOnInit(): void {}
  ngAfterViewInit(): void {
    const slider = this.sliderWrapper.nativeElement;
    slider.addEventListener('touchstart', this.onTouchStart.bind(this));
    slider.addEventListener('touchmove', this.onTouchMove.bind(this));
    slider.addEventListener('touchend', this.onTouchEnd.bind(this));
    this.startAutoplay();
  }
  startAutoplay(): void {
    this.autoplayInterval = setInterval(() => {
      this.goNext();
    }, 4000);
  }
  stopAutoplay(): void {
    if (this.autoplayInterval) {
      clearInterval(this.autoplayInterval);
    }
  }
  onTouchStart(event: TouchEvent) {
    this.startX = event.touches[0].clientX;
  }

  onTouchMove(event: TouchEvent) {
    this.endX = event.touches[0].clientX;
  }

  onTouchEnd() {
    if (this.startX > this.endX + 50) {
      this.goNext();
    } else if (this.startX < this.endX - 50) {
      this.goPrev();
    }
  }

  get transformStyle(): string {
    return `translateX(-${this.currentIndex * 100}%)`;
  }

  goNext(): void {
    if (this.currentIndex < this.homeStore.sliders$().length - 1) {
      this.currentIndex++;
    } else {
      this.currentIndex = 0;
    }
  }

  goPrev(): void {
    if (this.currentIndex > 0) {
      this.currentIndex--;
    } else {
      this.currentIndex = this.homeStore.sliders$().length - 1;
    }
  }
}
