import { CommonModule } from '@angular/common';
import { Component, Input, OnChanges } from '@angular/core';

@Component({
  selector: 'app-star-rating',
  templateUrl: './star-rating.component.html',
  styleUrls: ['./star-rating.component.scss'],
  standalone: true,
  imports: [CommonModule],
})
export class StarRatingComponent implements OnChanges {
  @Input() value: number = 0;
  @Input() starSize: number = 21;
  @Input() count!: number;
  fullStars: number[] = [];
  partialStarWidth: number = 0;
  emptyStars: number[] = [];

  ngOnChanges(): void {
    this.updateStars();
  }

  updateStars() {
    const fullStarsCount = Math.floor(this.value);
    const fraction = this.value - fullStarsCount;
    this.fullStars = Array(fullStarsCount).fill(0);
    this.partialStarWidth = fraction * 100;
    this.emptyStars = Array(5 - fullStarsCount - (fraction > 0 ? 1 : 0)).fill(
      0
    );
  }
}
