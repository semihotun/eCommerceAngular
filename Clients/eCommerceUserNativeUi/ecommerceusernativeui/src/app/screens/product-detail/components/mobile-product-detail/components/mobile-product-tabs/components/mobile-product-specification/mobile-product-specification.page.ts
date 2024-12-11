import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductStore } from 'src/app/stores/product.store';
import { ProductService } from 'src/app/services/product.service';
import { NavController, IonContent } from '@ionic/angular/standalone';
import { INT32_MAX_VALUE } from 'src/app/models/consts/contant';
import { GetProductDetailSpeficationListRequest } from 'src/app/models/requestModel/getProductDetailSpeficationListRequest';
import { SubHeaderComponent } from 'src/app/uı/sub-header/sub-header.component';
import { MobileFooterComponent } from 'src/app/uı/mobile-footer/mobile-footer.component';
import { TranslateModule } from '@ngx-translate/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-mobile-product-specification',
  templateUrl: './mobile-product-specification.page.html',
  styleUrls: ['./mobile-product-specification.page.scss'],
  standalone: true,
  imports: [
    IonContent,
    CommonModule,
    SubHeaderComponent,
    MobileFooterComponent,
    TranslateModule,
  ],
})
export class MobileProductSpecificationPage implements OnInit {
  slug: string = '';
  constructor(
    public productStore: ProductStore,
    public productService: ProductService,
    public navController: NavController,
    private activatedRoute: ActivatedRoute
  ) {}

  ngOnInit() {
    this.activatedRoute.paramMap.subscribe(async (params) => {
      this.slug = params.get('slug')!;
      if (this.productStore.selectedProductId$()) {
        const request = new GetProductDetailSpeficationListRequest();
        request.pageIndex = 1;
        request.pageSize = INT32_MAX_VALUE;
        request.productId = this.productStore.selectedProductId$();
        this.productService.getProductDetailSpeficationList(request);
      } else {
        this.navController.navigateBack(this.slug);
      }
    });
  }
}
