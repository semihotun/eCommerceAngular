import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ProductStore } from 'src/app/stores/product.store';

@Component({
  selector: 'app-product-specification',
  templateUrl: './product-specification.component.html',
  styleUrls: ['./product-specification.component.scss'],
  standalone: true,
  imports: [CommonModule],
})
export class ProductSpecificationComponent implements OnInit {
  constructor(public productStore: ProductStore) {}

  ngOnInit() {}
}
