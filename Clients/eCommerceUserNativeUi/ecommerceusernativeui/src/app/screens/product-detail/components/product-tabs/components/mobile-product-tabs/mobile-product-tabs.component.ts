import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { TranslateModule } from '@ngx-translate/core';
import { ProductStore } from 'src/app/stores/product.store';
import { RouterModule, ActivatedRoute } from '@angular/router';
import { NavController } from '@ionic/angular/standalone';
@Component({
  selector: 'app-mobile-product-tabs',
  templateUrl: './mobile-product-tabs.component.html',
  styleUrls: ['./mobile-product-tabs.component.scss'],
  standalone: true,
  imports: [CommonModule, TranslateModule, RouterModule],
})
export class MobileProductTabsComponent implements OnInit {
  slug: string = '';
  constructor(
    public productStore: ProductStore,
    public navController: NavController,
    private activatedRoute: ActivatedRoute
  ) {}

  ngOnInit() {
    this.activatedRoute.paramMap.subscribe(async (params) => {
      this.slug = params.get('slug')!;
    });
  }
}
