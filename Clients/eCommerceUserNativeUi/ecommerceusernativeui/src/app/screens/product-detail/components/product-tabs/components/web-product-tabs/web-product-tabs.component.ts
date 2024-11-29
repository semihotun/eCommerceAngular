import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { TranslateModule } from '@ngx-translate/core';
import { GlobalService } from 'src/app/services/core/global.service';
import { ProductStore } from 'src/app/stores/product.store';
import { CommentsPage } from '../../../../../comments/comments.page';
import { ProductQuestionAnswerComponent } from '../../../product-question-answer/product-question-answer.component';
import { ProductSpecificationComponent } from '../../../product-specification/product-specification.component';

@Component({
  selector: 'app-web-product-tabs',
  templateUrl: './web-product-tabs.component.html',
  styleUrls: ['./web-product-tabs.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
    TranslateModule,
    CommentsPage,
    ProductQuestionAnswerComponent,
    ProductSpecificationComponent,
  ],
})
export class WebProductTabsComponent implements OnInit {
  constructor(public productStore: ProductStore, public glb: GlobalService) {}

  ngOnInit() {}
  selectedTab: number = 1;

  selectTab(tabIndex: number): void {
    this.selectedTab = tabIndex;
  }
}
