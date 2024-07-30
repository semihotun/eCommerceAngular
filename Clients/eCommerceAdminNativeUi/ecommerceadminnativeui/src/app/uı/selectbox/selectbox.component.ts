import {
  Component,
  EventEmitter,
  forwardRef,
  inject,
  Input,
  OnInit,
  Output,
} from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import { ModalController, IonIcon } from '@ionic/angular/standalone';
import { SelectboxModalComponent } from './components/selectbox-modal/selectbox-modal.component';

@Component({
  selector: 'app-selectbox',
  templateUrl: './selectbox.component.html',
  styleUrls: ['./selectbox.component.scss'],
  standalone: true,
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => SelectboxComponent),
      multi: true,
    },
  ],
  imports: [IonIcon],
})
export class SelectboxComponent implements ControlValueAccessor {
  @Input() data!: any[];
  onChange: any = () => {};
  onTouched: any = () => {};
  val: any = null;
  modal = inject(ModalController);
  constructor() {}
  ngOnInit(): void {}
  public disabled: boolean = false;
  @Input() placeholder: string = '';
  @Input() label: string = '';
  @Input() class!: string;
  @Input() textProperty!: string;
  @Input() valueProperty!: string;
  @Output() change = new EventEmitter<any>();

  writeValue(obj: any): void {
    if (obj !== undefined && this.val !== obj) {
      //daha sonra değişicek
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
    this.disabled = isDisabled;
  }
  convertTypeText() {}
  async openModal() {
    const selectboxModal = await this.modal.create({
      component: SelectboxModalComponent,
      componentProps: {
        data: this.data,
        textProperty: this.textProperty,
        valueProperty: this.valueProperty,
        label: this.label,
      },
    });
    selectboxModal.present();
    selectboxModal.onDidDismiss().then((result) => {
      if (result.data) {
        this.val = result.data.text;
        this.onChange(result.data.value);
        this.onTouched();
        this.change.emit();
      }
    });
  }
}
