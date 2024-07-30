import { CommonModule } from '@angular/common';
import {
  Component,
  ElementRef,
  EventEmitter,
  inject,
  Input,
  OnDestroy,
  OnInit,
  Output,
  ViewChild,
} from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
} from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import {
  FilterModel,
  FilterOperators,
  GridPostData,
  GridPropertyInfo,
  ValueText,
} from 'src/app/models/core/grid';
import { GridService } from 'src/app/services/grid.service';
import { GridStore } from 'src/app/stores/grid.store';
import { ModalController } from '@ionic/angular/standalone';
import { GridPropertyModalComponent } from './components/grid-property-modal/grid-property-modal.component';
import { CheckboxComponent } from '../checkbox/checkbox.component';
import { BtnSubmitComponent } from '../btn-submit/btn-submit.component';
import { takeUntil, Subject } from 'rxjs';
import { SelectboxComponent } from '../selectbox/selectbox.component';
import { InputComponent } from '../input/input.component';
@Component({
  selector: 'app-grid',
  templateUrl: './grid.component.html',
  styleUrls: ['./grid.component.scss'],
  standalone: true,
  imports: [
    TranslateModule,
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    CheckboxComponent,
    BtnSubmitComponent,
    SelectboxComponent,
    InputComponent,
  ],
})
export class GridComponent implements OnInit {
  @Input() url!: string;
  @Input() btnColumnOn: Boolean = false;
  @Input() editBtnUrl!: string;
  @Input() deleteBtnUrl!: string;
  @ViewChild('prevBtn') prevBtn!: ElementRef<HTMLElement>;
  @ViewChild('nextBtn') nextBtn!: ElementRef<HTMLElement>;
  @Output() getAllDataVoid: EventEmitter<void> = new EventEmitter<void>();

  datas!: any[];
  goToPageIndex!: number;
  keys!: GridPropertyInfo[];
  filter: FilterModel = new FilterModel();
  filters!: ValueText[];
  filterList: FilterModel[] = [];
  gridPostData: GridPostData = new GridPostData(1, 10);
  gridStore = inject(GridStore);
  gridService = inject(GridService);
  filterForm!: FormGroup;

  constructor(
    private modalController: ModalController,
    private builder: FormBuilder
  ) {}

  ngAfterViewInit() {}

  initForm() {
    this.filterForm = this.builder.group({
      propertyName: ['', []],
      filterType: ['', []],
      filterValue: ['', []],
      jsonOrXml: [false, []],
      andOrOperation: ['', []],
    });
  }

  async ngOnInit() {
    this.initForm();
    await this.gridService.getSettingsGrid(this.url);
    await this.getAllList();
  }

  async getAllList() {
    if (!this.getAllDataVoid.observed) {
      await this.gridService.getAllData(this.url, this.gridPostData);
    } else {
      this.getAllDataVoid.emit();
    }
    this.datas = this.gridStore.dataSignal$().data;
    this.propertyList();
    this.gridPostData.pageIndex = this.gridStore.dataSignal$().pageIndex;
    this.gridPostData.pageSize = this.gridStore.dataSignal$().pageSize;
    this.prevDisabled();
    this.nextDisabled();
  }

  changeFilterName(value: FilterModel) {
    return (
      value.propertyName +
      ' ' +
      FilterOperators[+value.filterType] +
      ' ' +
      value.filterValue
    );
  }

  async deleteData(id: number) {
    await this.gridService.deleteUrl(this.deleteBtnUrl, id);
    await this.getAllList();
  }

  propertyList() {
    if (this.gridStore.gridSettings$() != null) {
      const splitData =
        this.gridStore.gridSettings$().propertyInfo.split(',') ?? [];

      this.keys = this.gridStore
        .dataSignal$()
        .propertyInfos.map((propertyInfo) => {
          propertyInfo.checked = splitData.includes(propertyInfo.propertyName);
          return propertyInfo;
        });
    } else {
      this.keys = this.gridStore
        .dataSignal$()
        .propertyInfos.map((propertyInfo) => {
          propertyInfo.checked = true;
          return propertyInfo;
        });
    }
  }

  prevClick() {
    if (this.gridPostData.pageIndex >= 1) {
      this.gridPostData.pageIndex = this.gridPostData.pageIndex - 1;
      this.getAllList();
    }
  }

  nextClick() {
    if (
      this.gridPostData.pageIndex <= this.gridStore.dataSignal$().totalPages
    ) {
      this.gridPostData.pageIndex = this.gridPostData.pageIndex + 1;
      this.getAllList();
    }
  }

  prevDisabled() {
    if (this.gridPostData.pageIndex <= 1) {
      this.prevBtn.nativeElement.className = 'prevNextDisabled prev-btn';
    } else {
      this.prevBtn.nativeElement.className = 'prev-btn';
    }
  }

  nextDisabled() {
    if (
      this.gridPostData.pageIndex >= this.gridStore.dataSignal$().totalPages
    ) {
      this.nextBtn.nativeElement.className = 'prevNextDisabled next-btn';
    } else {
      this.nextBtn.nativeElement.className = 'next-btn';
    }
  }

  pageSizeChange(event: any) {
    this.gridPostData.pageSize = event.target.value;
    this.getAllList();
  }

  changePageIndex() {
    this.gridPostData.pageIndex = this.goToPageIndex;
    this.getAllList();
  }

  changeOrderBy(orderBy: string) {
    let orderByPropertyName = this.toFirstUpperString(orderBy);
    if (this.gridPostData.orderByColumnName.includes('desc') == true) {
      this.gridPostData.orderByColumnName = orderByPropertyName;
    } else {
      this.gridPostData.orderByColumnName = orderByPropertyName + ',desc';
    }
    this.getAllList();
  }

  toFirstUpperString(data: string) {
    return data.charAt(0).toUpperCase() + data.slice(1);
  }

  changeFilter() {
    let propertyName = this.filterForm.get('propertyName')?.value;
    let propertyType = this.keys.find(
      (x) => x.propertyName == propertyName
    )?.propertyType;
    this.filterForm.patchValue({
      filterType: '',
      filterValue: '',
    });
    let filter: ValueText[] = [];
    switch (propertyType) {
      case 'Int32':
        filter.push({ value: '1', text: 'Eşit' });
        filter.push({ value: '2', text: 'Eşit Değil' });
        filter.push({ value: '5', text: 'Büyüktür' });
        filter.push({ value: '6', text: 'Küçüktür' });
        break;
      case 'Guid':
        filter.push({ value: '1', text: 'Eşit' });
        break;
      case 'String':
        filter.push({ value: '1', text: 'Eşit' });
        filter.push({ value: '2', text: 'Eşit Değil' });
        filter.push({ value: '3', text: 'İçerir' });
        break;
      case 'DateTime':
        filter.push({ value: '1', text: 'Eşit' });
        break;
      case 'Boolean':
        filter.push({ value: 'true', text: 'Evet' });
        filter.push({ value: 'false', text: 'Hayır' });
        break;
      case 'Json':
        filter.push({ value: '3', text: 'İçerir' });
        break;
      case 'Xml':
        filter.push({ value: '3', text: 'İçerir' });
        break;
    }
    this.filters = filter;
  }

  addFilter() {
    this.filterList.push(this.filterForm.value);
    this.gridPostData.filterModelList = this.filterList;
    this.getAllList();
  }

  deleteFilter(deleteFilter: FilterModel) {
    this.filterList = this.filterList.filter((x) => x != deleteFilter);
    this.gridPostData.filterModelList = this.filterList;
    if (this.filterList.length >= 1) this.filterList[0].andOrOperation = '';
    this.getAllList();
  }

  async propertyModal() {
    const modal = await this.modalController.create({
      component: GridPropertyModalComponent,
      componentProps: {
        keys: this.keys,
        path: this.url,
      },
    });
    modal.onDidDismiss().then((result) => {
      if (result) {
        this.propertyList();
      }
    });
    modal.present();
  }
}
