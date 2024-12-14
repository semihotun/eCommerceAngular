import {
  Component,
  ElementRef,
  EventEmitter,
  forwardRef,
  inject,
  Input,
  Output,
  ViewChild,
} from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import {
  ModalController,
  IonIcon,
  IonToolbar,
  IonHeader,
  IonContent,
  IonTitle,
  IonButton,
  IonButtons,
  IonInfiniteScroll,
  IonInfiniteScrollContent,
  IonItem,
  IonModal,
} from '@ionic/angular/standalone';
import { InfiniteScrollCustomEvent } from '@ionic/core';
import { TranslateModule } from '@ngx-translate/core';
import { CommonModule } from '@angular/common';
import { PagedList } from 'src/app/models/core/grid';

@Component({
  selector: 'app-pagination-selectbox',
  templateUrl: './pagination-selectbox.component.html',
  styleUrls: ['./pagination-selectbox.component.scss'],
  standalone: true,
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => PaginationSelectboxComponent),
      multi: true,
    },
  ],
  imports: [
    IonModal,
    IonItem,
    IonInfiniteScrollContent,
    IonInfiniteScroll,
    IonContent,
    IonIcon,
    TranslateModule,
    CommonModule,
  ],
})
export class PaginationSelectboxComponent implements ControlValueAccessor {
  @Input() pagedListData!: PagedList<any>;
  onChange: any = () => {};
  onTouched: any = () => {};
  val: any = null;
  constructor() {}
  ngOnInit(): void {}
  public disabled: boolean = false;
  @Input() placeholder: string = '';
  @Input() label: string = '';
  @Input() class!: string;
  @Input() textProperty!: string;
  @Input() valueProperty!: string;
  @Output() change = new EventEmitter<any>();
  @Output() searchableTriggered = new EventEmitter<{
    pageIndex: number;
    pageSize: number;
    filterText: string;
    replace: boolean;
  }>();
  @ViewChild(IonModal) modal!: IonModal;
  filterText!: string;
  writeValue(obj: any): void {
    if (obj !== undefined && this.val !== obj) {
      const foundItem =
        this.textProperty && this.valueProperty
          ? this.pagedListData.data?.find(
              (item) => item[this.valueProperty] === obj
            )
          : null;

      this.val = foundItem ? foundItem[this.textProperty] : obj;

      this.onChange(obj);
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

  onIonInfinite(ev: InfiniteScrollCustomEvent) {
    setTimeout(() => {
      this.searchableTriggered.emit({
        pageIndex: ++this.pagedListData.pageIndex,
        pageSize: this.pagedListData.pageSize,
        filterText: this.filterText,
        replace: false,
      });
      ev.target.complete();
    }, 1000);
  }

  closeModal() {
    this.modal.dismiss();
  }
  selectedItem(item: any) {
    this.val = item[this.textProperty] || item;
    this.onChange(item[this.valueProperty] || item);
    this.onTouched();
    this.change.emit(item[this.valueProperty] || item);
    this.modal.dismiss();
  }
  changeSearchBox(event: any) {
    this.filterText = event.target.value;
    this.searchableTriggered.emit({
      pageIndex: 1,
      pageSize: this.pagedListData.pageSize,
      filterText: this.filterText,
      replace: true,
    });
  }
}
