import { CommonModule } from '@angular/common';
import { Component, forwardRef, Input, OnInit } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
  selector: 'app-input-star-rating',
  templateUrl: './input-star-rating.component.html',
  styleUrls: ['./input-star-rating.component.scss'],
  standalone: true,
  imports: [CommonModule],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => InputStarRatingComponent),
      multi: true,
    },
  ],
})
export class InputStarRatingComponent implements ControlValueAccessor {
  @Input() starSize: number = 24;
  @Input() maxStars: number = 5;

  stars: number[] = [];
  value: number = 0;

  onTouched: () => void = () => {};
  onChange: (value: number) => void = () => {};

  constructor() {
    this.stars = Array.from({ length: this.maxStars }, (_, i) => i + 1);
  }

  writeValue(value: number): void {
    this.value = value || 0;
  }

  registerOnChange(fn: (value: number) => void): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: () => void): void {
    this.onTouched = fn;
  }
  selectStar(star: number): void {
    this.value = star;
    this.onChange(this.value);
    this.onTouched();
  }
}
