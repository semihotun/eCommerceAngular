import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { TranslateModule } from '@ngx-translate/core';
import { GlobalService } from 'src/app/services/core/global.service';
import { ProductStore } from 'src/app/stores/product.store';
import { ProductSpecificationComponent } from './components/product-specification/product-specification.component';
import { ProductService } from 'src/app/services/product.service';
import { GetProductDetailSpeficationListRequest } from 'src/app/models/requestModel/getProductDetailSpeficationListRequest';
import { INT32_MAX_VALUE } from 'src/app/models/consts/contant';
import { ProductCommentsComponent } from './components/product-comments/product-comments.component';
import { ProductQuestionAnswerComponent } from './components/product-question-answer/product-question-answer.component';
@Component({
  selector: 'app-web-product-tabs',
  templateUrl: './web-product-tabs.component.html',
  styleUrls: ['./web-product-tabs.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
    TranslateModule,
    ProductQuestionAnswerComponent,
    ProductSpecificationComponent,
    ProductCommentsComponent,
  ],
})
export class WebProductTabsComponent implements OnInit {
  constructor(
    public productStore: ProductStore,
    public glb: GlobalService,
    public productService: ProductService
  ) {}

  ngOnInit() {}
  selectedTab: number = 4;

  selectTab(tabIndex: number): void {
    this.selectedTab = tabIndex;
    if (this.selectedTab == 2) {
      const request = new GetProductDetailSpeficationListRequest();
      request.pageIndex = 1;
      request.pageSize = INT32_MAX_VALUE;
      request.productId = this.productStore.selectedProductId$();
      this.productService.getProductDetailSpeficationList(request);
    }
  }
}
