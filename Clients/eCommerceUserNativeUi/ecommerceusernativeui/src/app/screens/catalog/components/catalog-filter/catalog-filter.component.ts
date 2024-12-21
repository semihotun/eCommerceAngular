import {
  Component,
  EventEmitter,
  Input,
  OnDestroy,
  OnInit,
  Output,
  QueryList,
  ViewChild,
  ViewChildren,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormControl,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { IonContent, IonMenu } from '@ionic/angular/standalone';
import { TranslateModule } from '@ngx-translate/core';
import { CatalogStore } from './../../../../stores/catalog.store';
import { CheckboxComponent } from '../../../../u\u0131/checkbox/checkbox.component';
import { CatalogFilter } from 'src/app/models/requestModel/getCatalogProductDTO';
import { InputComponent } from 'src/app/uı/input/input.component';
import { Observable, Subject, takeUntil, pipe, BehaviorSubject } from 'rxjs';
@Component({
  selector: 'app-catalog-filter',
  templateUrl: './catalog-filter.component.html',
  styleUrls: ['./catalog-filter.component.scss'],
  standalone: true,
  imports: [
    IonContent,
    TranslateModule,
    CommonModule,
    FormsModule,
    IonMenu,
    CheckboxComponent,
    InputComponent,
    ReactiveFormsModule,
    FormsModule,
    CommonModule,
  ],
})
export class CatalogFilterComponent implements OnDestroy, OnInit {
  @ViewChild('filterMenu') filterMenu!: IonMenu;
  minPrice = 0;
  maxPrice = 0;
  filterList: CatalogFilter[] = [];
  @ViewChildren(CheckboxComponent) checkboxes!: QueryList<CheckboxComponent>;
  @Output() resetAndgetCatalogProductDtoBySlug = new EventEmitter<any>();
  onDestroy = new Subject<void>();
  constructor(public catalogStore: CatalogStore) {}
  ionViewWillEnter() {}
  ngOnInit(): void {
    this.catalogStore.removedOptionId$
      .pipe(takeUntil(this.onDestroy))
      .subscribe((x) => {
        this.filterList = this.filterList.filter(
          (z) => z.specificationAttributeOptionId !== x
        );
        this.checkboxes.forEach((checkbox) => {
          if (x.includes(checkbox.data)) {
            checkbox.val = false;
          }
        });
      });
  }
  ngOnDestroy(): void {
    this.onDestroy.next();
    this.onDestroy.complete();
  }

  openFilterMenu() {
    this.filterMenu.open();
  }
  closeFilter() {
    this.filterMenu.close();
  }
  showSubNodeCategory(e: EventTarget) {
    const target = (e as HTMLElement).nextElementSibling as HTMLElement;
    if (!target.classList.contains('sub-category-close')) {
      target.classList.replace('sub-category-open', 'sub-category-close');
    } else if (target.classList.contains('sub-category-close')) {
      target.classList.replace('sub-category-close', 'sub-category-open');
    }
  }
  saveFilter() {
    this.catalogStore.setFilterList(this.filterList);
    this.resetAndgetCatalogProductDtoBySlug.emit();
    this.filterMenu.close();
  }
  addPriceFilter() {
    this.catalogStore.setMinPrice(this.minPrice);
    this.catalogStore.setMaxPrice(this.maxPrice);
  }
  onCheckboxChange(
    specificationAttributeOptionId: string,
    specificationAttributeId: string,
    specificationAttributeOptionName: string
  ) {
    let isChecked = this.filterList.find(
      (item) =>
        item.specificationAttributeOptionId == specificationAttributeOptionId
    );
    if (!isChecked) {
      let filterData = new CatalogFilter();
      filterData.specificationAttributeOptionId =
        specificationAttributeOptionId;
      filterData.specificationAttributeId = specificationAttributeId;
      filterData.specificationAttributeOptionName =
        specificationAttributeOptionName;
      this.filterList.push(filterData);
    } else {
      this.filterList = this.filterList.filter(
        (item) =>
          item.specificationAttributeOptionId !== specificationAttributeOptionId
      );
    }
  }
}
