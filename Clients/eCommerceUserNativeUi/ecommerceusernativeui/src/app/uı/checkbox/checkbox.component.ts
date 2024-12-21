import {
  Component,
  EventEmitter,
  forwardRef,
  Input,
  OnInit,
  Output,
} from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
  selector: 'app-checkbox',
  templateUrl: './checkbox.component.html',
  styleUrls: ['./checkbox.component.scss'],
  standalone: true,
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => CheckboxComponent),
      multi: true,
    },
  ],
})
export class CheckboxComponent implements ControlValueAccessor {
  constructor() {}
  onChange: any = () => {};
  onTouched: any = () => {};
  @Input() text: string = '';
  @Input() data: any;
  // @Output() change: EventEmitter<{ data: any; checked: boolean }> =
  //   new EventEmitter<{ data: any; checked: boolean }>();
  val: boolean = false;
  disabled: boolean = false;
  ngOnInit() {}
  writeValue(obj: any): void {
    this.val = obj;
    this.onChange(this.val);
    this.onTouched();
  }
  registerOnChange(fn: any): void {
    this.onChange = fn;
  }
  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }
  setDisabledState?(isDisabled: boolean): void {
    this.disabled = isDisabled;
  }
  onCheckboxChange() {
    this.val = !this.val;
    this.onTouched();
    console.log(this.val);
    this.onChange(this.val);
    if (this.data) {
      // this.change.emit({ data: this.data, checked: this.val });
    } else {
    }
  }
}
