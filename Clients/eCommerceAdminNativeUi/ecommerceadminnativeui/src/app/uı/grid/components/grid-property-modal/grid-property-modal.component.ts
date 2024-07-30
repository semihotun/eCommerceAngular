import { CommonModule } from '@angular/common';
import { Component, inject, Input, OnDestroy, OnInit } from '@angular/core';
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
import {
  GridPropertyInfo,
  GridSettingsDTO,
  PagedList,
} from 'src/app/models/core/grid';
import { GridService } from 'src/app/services/core/grid.service';
import { GridStore } from 'src/app/stores/grid.store';
import { CheckboxComponent } from '../../../checkbox/checkbox.component';
import { BehaviorSubject, Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-grid-property-modal',
  templateUrl: './grid-property-modal.component.html',
  styleUrls: ['./grid-property-modal.component.scss'],
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
    TranslateModule,
    CheckboxComponent,
  ],
})
export class GridPropertyModalComponent implements OnInit, OnDestroy {
  @Input() keys!: GridPropertyInfo[];
  @Input() path!: string;
  modalController = inject(ModalController);
  @Input() gridStore!: GridStore;
  @Input() gridService!: GridService;
  @Input() datas: BehaviorSubject<PagedList<any[]> | null> =
    new BehaviorSubject<PagedList<any[]> | null>(null);
  constructor() {}
  onDestroy = new Subject<void>();
  ngOnDestroy(): void {
    this.onDestroy.unsubscribe();
  }

  ngOnInit() {
    if (this.gridStore.gridSettings$() == null) {
      let gridSettings = new GridSettingsDTO();

      this.datas.pipe(takeUntil(this.onDestroy)).subscribe((x) => {
        gridSettings.propertyInfo = x!.propertyInfos
          .map((x) => x.propertyName)
          .join(',');
        gridSettings.path = this.path;
        this.gridStore.setGridSettings(gridSettings);
      });
    }
  }
  cancel() {
    this.modalController.dismiss(false);
  }
  confirm() {
    this.gridService.updateGridSettings();
    this.modalController.dismiss(true);
  }
  changeCheckBoxProperty(data: string) {
    if (this.gridStore.gridSettings$().propertyInfo) {
      let splitData = this.gridStore.gridSettings$().propertyInfo.split(',');
      if (splitData.indexOf(data) == -1) {
        this.gridStore.setGridSettingsPropertyInfo(
          this.gridStore.gridSettings$().propertyInfo + ',' + data
        );
      } else {
        let joinData = splitData.filter((x) => x != data).join(',');
        if (joinData.length > 0)
          this.gridStore.setGridSettingsPropertyInfo(joinData);
        else
          this.gridStore.setGridSettingsPropertyInfo(
            joinData.substring(0, data.length - 1)
          );
      }
    } else {
      this.gridStore.setGridSettingsPropertyInfo(data);
    }
  }
}
