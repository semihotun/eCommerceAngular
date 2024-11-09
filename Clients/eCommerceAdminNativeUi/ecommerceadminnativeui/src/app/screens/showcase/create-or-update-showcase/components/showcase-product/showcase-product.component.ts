import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';
import { ShowCaseProduct } from 'src/app/models/responseModel/showcaseProduct';
import { ShowcaseService } from 'src/app/services/showcase/showcase.service';
import { ShowcaseStore } from 'src/app/stores/showcase.store';
import { GridComponent } from 'src/app/uı/grid/grid.component';
import { ShowcaseProductService } from './../../../../../services/showcase/showcase-product.service';
import { GridPostData } from 'src/app/models/core/grid';
import { BehaviorSubject, Subject, takeUntil } from 'rxjs';
@Component({
  selector: 'app-showcase-product',
  templateUrl: './showcase-product.component.html',
  styleUrls: ['./showcase-product.component.scss'],
  standalone: true,
  imports: [TranslateModule, GridComponent],
})
export class ShowcaseProductComponent implements OnInit, OnDestroy {
  showCaseId!: string;
  data: BehaviorSubject<any> = new BehaviorSubject<any>(null);
  onDestroy: Subject<void> = new Subject<void>();
  @ViewChild('showcaseProductGrid') showcaseProductGrid!: GridComponent;
  constructor(
    private activatedRoute: ActivatedRoute,
    private showCaseStore: ShowcaseStore,
    private showCaseProductService: ShowcaseProductService
  ) {}
  ngOnDestroy(): void {
    this.onDestroy.next();
    this.onDestroy.complete();
  }
  ngOnInit() {
    this.activatedRoute.paramMap.subscribe(async (params) => {
      if (params.get('id')) {
        this.showCaseId = params.get('id')!;
        this.initGrid();
      }
    });
  }
  initGrid() {
    this.showCaseStore.showcaseProductGridObservable$
      .pipe(takeUntil(this.onDestroy))
      .subscribe((x) => {
        if (x) {
          this.data.next(x);
        }
      });
  }
  async addShowcaseProduct(data: any) {
    var showcaseProduct = new ShowCaseProduct();
    showcaseProduct.productId = data.id;
    showcaseProduct.showCaseId = this.showCaseId;
    showcaseProduct.languageCode = this.showCaseStore.showcase$().languageCode;
    await this.showCaseProductService.createShowcaseProduct(showcaseProduct);
    this.showcaseProductGrid.refresh();
  }
  async getAllData(data?: GridPostData) {
    await this.showCaseProductService.getShowCaseProductGrid({
      ...data,
      showCaseId: this.showCaseId,
    });
  }
}
