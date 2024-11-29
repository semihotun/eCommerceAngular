import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { GlobalService } from 'src/app/services/core/global.service';
import { ProductStore } from 'src/app/stores/product.store';
import { WebProductTabsComponent } from './components/web-product-tabs/web-product-tabs.component';
import { MobileProductTabsComponent } from './components/mobile-product-tabs/mobile-product-tabs.component';

@Component({
  selector: 'app-product-tabs',
  templateUrl: './product-tabs.component.html',
  styleUrls: ['./product-tabs.component.scss'],
  standalone: true,
  imports: [CommonModule, WebProductTabsComponent, MobileProductTabsComponent],
})
export class ProductTabsComponent implements OnInit {
  constructor(public productStore: ProductStore, public glb: GlobalService) {}

  ngOnInit() {}
}
