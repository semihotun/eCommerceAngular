import { CommonModule } from '@angular/common';
import { Component, inject, Input, OnInit } from '@angular/core';
import {
  IonHeader,
  IonToolbar,
  IonButton,
  IonTitle,
  IonButtons,
  IonInput,
  IonItem,
  IonContent,
  IonIcon,
  ModalController,
} from '@ionic/angular/standalone';
import { TranslateModule } from '@ngx-translate/core';
import { CheckboxComponent } from 'src/app/uı/checkbox/checkbox.component';
@Component({
  selector: 'app-selectbox-modal',
  templateUrl: './selectbox-modal.component.html',
  styleUrls: ['./selectbox-modal.component.scss'],
  standalone: true,
  imports: [
    IonItem,
    IonInput,
    CommonModule,
    IonHeader,
    IonToolbar,
    IonButton,
    IonTitle,
    IonButtons,
    IonContent,
    IonIcon,
    CheckboxComponent,
    TranslateModule,
  ],
})
export class SelectboxModalComponent implements OnInit {
  @Input() textProperty!: string;
  @Input() valueProperty!: string;
  @Input() data!: any[];
  @Input() label: string = '';
  modalController = inject(ModalController);
  filterData!: any[];
  constructor() {}

  ngOnInit() {
    this.filterData = this.data;
  }
  cancel() {
    this.modalController.dismiss();
  }
  selectedItem(item: any) {
    let value = item[this.valueProperty] || item;
    let text = item[this.textProperty] || item;
    this.modalController.dismiss({ value: value, text: text });
  }
  changeSearchBox(event: any) {
    if (this.textProperty) {
      this.filterData = this.data.filter((x) =>
        x[this.textProperty].toLowerCase().includes(event.target.value)
      );
    } else {
      this.filterData = this.data.filter((x) =>
        x.toLowerCase().includes(event.target.value)
      );
    }
  }
}
