import { Component, forwardRef, inject, Input, OnInit } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import {
  IonSegmentButton,
  IonIcon,
  IonSegment,
  IonLabel,
} from '@ionic/angular/standalone';
import { TranslateService } from '@ngx-translate/core';
import { LanguageList } from 'src/app/models/consts/languagelist';
@Component({
  selector: 'app-segment-language',
  templateUrl: './segment-language.component.html',
  styleUrls: ['./segment-language.component.scss'],
  standalone: true,
  imports: [IonSegment, IonSegmentButton, IonIcon, IonLabel],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => SegmentLanguageComponent),
      multi: true,
    },
  ],
})
export class SegmentLanguageComponent implements ControlValueAccessor, OnInit {
  languages = LanguageList;
  constructor() {}
  value: string = 'tr';
  onChange: any = () => {};
  onTouched: any = () => {};
  translateService = inject(TranslateService);
  @Input() disabled: boolean = true;
  writeValue(obj: any): void {
    if (obj !== undefined && obj !== '') {
      this.value = obj;
      this.onChange(this.value);
      this.onTouched();
    } else {
      this.value = this.translateService.currentLang ?? 'tr';
      this.onChange(this.value);
      this.onTouched();
    }
  }
  registerOnChange(fn: any): void {
    this.onChange = fn;
  }
  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }
  setDisabledState?(isDisabled: boolean): void {
    // throw new Error('Method not implemented.');
  }

  ngOnInit() {}
  onSegmentChange(event: any) {
    this.value = event.target.value;
    this.onChange(this.value);
    this.onTouched();
  }
}
