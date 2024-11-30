import { Component, OnInit } from '@angular/core';
import { ProductSpecificationService } from 'src/app/services/product-specification.service';
import { ProductSpecificationStore } from 'src/app/stores/product-specification.store';

@Component({
  selector: 'app-product-specification',
  templateUrl: './product-specification.component.html',
  styleUrls: ['./product-specification.component.scss'],
  standalone: true,
})
export class ProductSpecificationComponent implements OnInit {
  constructor(
    private productSpecificationStore: ProductSpecificationStore,
    private productSpecificationService: ProductSpecificationService
  ) {}

  ngOnInit() {}
}
