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
  PagedList,
  ValueText,
} from 'src/app/models/core/grid';
import { GridService } from 'src/app/services/core/grid.service';
import { GridStore } from 'src/app/stores/grid.store';
import { ModalController } from '@ionic/angular/standalone';
import { GridPropertyModalComponent } from './components/grid-property-modal/grid-property-modal.component';
import { CheckboxComponent } from '../checkbox/checkbox.component';
import { BtnSubmitComponent } from '../btn-submit/btn-submit.component';
import { SelectboxComponent } from '../selectbox/selectbox.component';
import { InputComponent } from '../input/input.component';
import { BehaviorSubject, Subject, takeUntil } from 'rxjs';
import { LazyImgDirective } from './../../directives/lazy-img.directive';
import { environment } from 'src/environments/environment';
@Component({
  selector: 'app-grid',
  templateUrl: './grid.component.html',
  styleUrls: ['./grid.component.scss'],
  standalone: true,
  providers: [GridStore, GridService],
  imports: [
    TranslateModule,
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    BtnSubmitComponent,
    SelectboxComponent,
    InputComponent,
    LazyImgDirective,
  ],
})
export class GridComponent implements OnInit, OnDestroy {
  @Input() url!: string;
  @Input() btnColumnOn: Boolean = false;
  @Input() editBtnUrl!: string;
  @Input() deleteBtnUrl!: string;
  @ViewChild('prevBtn') prevBtn!: ElementRef<HTMLElement>;
  @ViewChild('nextBtn') nextBtn!: ElementRef<HTMLElement>;
  @Output() getAllDataVoid: EventEmitter<GridPostData> =
    new EventEmitter<GridPostData>();
  @Output() addBtn: EventEmitter<any> = new EventEmitter<any>();
  @Output() deleteBtn: EventEmitter<any> = new EventEmitter<any>();
  @Input() datas: BehaviorSubject<PagedList<any[]> | null> =
    new BehaviorSubject<PagedList<any[]> | null>(null);
  localData: any[] = [];
  onDestroy = new Subject<void>();
  goToPageIndex!: number;
  keys!: GridPropertyInfo[];
  filter: FilterModel = new FilterModel();
  filters!: ValueText[];
  filterList: FilterModel[] = [];
  gridPostData: GridPostData = new GridPostData(1, 10);
  gridStore = inject(GridStore);
  gridService = inject(GridService);
  filterForm!: FormGroup;
  totalPages: number = 1;
  constructor(
    private modalController: ModalController,
    private builder: FormBuilder
  ) {}
  refresh(): void {
    this.ngOnInit();
  }
  ngOnDestroy(): void {
    this.onDestroy.next();
    this.onDestroy.complete();
    this.onDestroy.unsubscribe();
  }

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
    this.datas.pipe(takeUntil(this.onDestroy)).subscribe((x) => {
      if (x) {
        this.totalPages = x.totalPages;
        this.localData = x.data;
        this.propertyList();
        this.gridPostData.pageIndex = x.pageIndex;
        this.gridPostData.pageSize = x.pageSize;
        this.prevDisabled();
        this.nextDisabled();
      }
    });
  }

  async getAllList() {
    if (!this.getAllDataVoid.observed) {
      await this.gridService.getAllData(this.url, this.gridPostData);
      this.datas.next(this.gridStore.dataSignal$());
    } else {
      this.getAllDataVoid.emit(this.gridPostData);
    }
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
    if (this.deleteBtn.observed) {
      this.deleteBtn.emit(id);
    } else {
      await this.gridService.deleteUrl(this.deleteBtnUrl, id);
      await this.getAllList();
    }
  }
  getImagePath(path: string) {
    if (path.startsWith('http://') || path.startsWith('https://')) {
      return path;
    }
    return environment.photoPath + path;
  }
  propertyList() {
    if (this.gridStore.gridSettings$() != null) {
      const splitData =
        this.gridStore.gridSettings$().propertyInfo.split(',') ?? [];

      this.datas.pipe(takeUntil(this.onDestroy)).subscribe((x) => {
        if (x!.propertyInfos.length > 0) {
          this.keys = x!.propertyInfos.map((propertyInfo) => {
            propertyInfo.checked = splitData.includes(
              propertyInfo.propertyName
            );
            return propertyInfo;
          });
        }
      });
    } else {
      this.datas.pipe(takeUntil(this.onDestroy)).subscribe((x) => {
        if (x) {
          this.keys = x!.propertyInfos.map((propertyInfo) => {
            propertyInfo.checked = true;
            return propertyInfo;
          });
        }
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
    if (this.gridPostData.pageIndex <= this.totalPages) {
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
    if (this.gridPostData.pageIndex >= this.totalPages) {
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
        datas: this.datas,
        gridStore: this.gridStore,
        gridService: this.gridService,
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
