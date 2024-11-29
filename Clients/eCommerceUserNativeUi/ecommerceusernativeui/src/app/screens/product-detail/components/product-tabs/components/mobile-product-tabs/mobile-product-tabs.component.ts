import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { TranslateModule } from '@ngx-translate/core';
import { ProductStore } from 'src/app/stores/product.store';
import { ProductCommentsComponent } from '../../../product-comments/product-comments.component';
import { ProductQuestionAnswerComponent } from '../../../product-question-answer/product-question-answer.component';
import { ProductSpecificationComponent } from '../../../product-specification/product-specification.component';

@Component({
  selector: 'app-mobile-product-tabs',
  templateUrl: './mobile-product-tabs.component.html',
  styleUrls: ['./mobile-product-tabs.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
    TranslateModule,
    ProductCommentsComponent,
    ProductQuestionAnswerComponent,
    ProductSpecificationComponent,
  ],
})
export class MobileProductTabsComponent implements OnInit {
  constructor(public productStore: ProductStore) {}

  ngOnInit() {}
}
