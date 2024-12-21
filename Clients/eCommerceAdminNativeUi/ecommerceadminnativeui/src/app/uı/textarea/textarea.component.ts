import { Component, forwardRef, Input, OnInit } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';

@Component({
  selector: 'app-textarea',
  templateUrl: './textarea.component.html',
  styleUrls: ['./textarea.component.scss'],
  standalone: true,
  imports: [TranslateModule],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => TextareaComponent),
      multi: true,
    },
  ],
})
export class TextareaComponent implements ControlValueAccessor, OnInit {
  constructor() {}
  ngOnInit(): void {}
  onChange: any = () => {};
  onTouched: any = () => {};
  val: any = null;

  @Input() disabled: boolean = false;
  @Input() placeholder: string = '';
  @Input() label: string = '';
  @Input() id!: string;
  @Input() class!: string;
  @Input() rows: number = 2;
  writeValue(obj: any): void {
    if (obj !== undefined && this.val !== obj) {
      this.val = obj;
      this.onChange(this.val);
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
    // this.disabled = isDisabled;
  }

  onInputChange(event: any): void {
    const value = event.target.value;
    this.val = value;
    this.onChange(value);
    this.onTouched();
  }
}
