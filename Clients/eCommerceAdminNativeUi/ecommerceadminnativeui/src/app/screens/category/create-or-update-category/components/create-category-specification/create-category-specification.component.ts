import { Component, inject, Input, OnInit, ViewChild } from '@angular/core';
import { TranslateModule } from '@ngx-translate/core';
import { GridComponent } from '../../../../../u\u0131/grid/grid.component';
import { CategorySpecificationStore } from 'src/app/stores/category-specification.store';
import { BehaviorSubject, Subject, takeUntil } from 'rxjs';
import { CategorySpecificationService } from './../../../../../services/category/category-specification.service';
import { GridPostData } from 'src/app/models/core/grid';
import { CategorySpecification } from 'src/app/models/responseModel/categorySpecification';
import { CategorySpecificationGridComponent } from '../category-specification-grid/category-specification-grid.component';
@Component({
  selector: 'app-create-category-specification',
  templateUrl: './create-category-specification.component.html',
  styleUrls: ['./create-category-specification.component.scss'],
  standalone: true,
  imports: [TranslateModule, GridComponent, CategorySpecificationGridComponent],
})
export class CreateCategorySpecificationComponent implements OnInit {
  @Input('categoryId') categoryId!: string;
  categorySpecificationStore = inject(CategorySpecificationStore);
  data: BehaviorSubject<any> = new BehaviorSubject<any>(null);
  onDestroy: Subject<void> = new Subject<void>();
  categorySpecificationService = inject(CategorySpecificationService);
  @ViewChild('categorySpecGrid')
  categorySpecGrid!: CategorySpecificationGridComponent;
  @ViewChild('addCategorySpecificationGrid')
  addCategorySpecificationGrid!: GridComponent;
  constructor() {}
  ngOnDestroy(): void {
    this.onDestroy.next();
    this.onDestroy.complete();
  }

  async getAllData(data?: GridPostData) {
    await this.categorySpecificationService.getAllNotExistSpecificationGridDTO(
      this.categoryId,
      data!
    );
  }
  async ngOnInit() {
    this.categorySpecificationStore.getAllNotExistSpecificationGridDTOObservable$
      .pipe(takeUntil(this.onDestroy))
      .subscribe((x) => {
        if (x) {
          this.data.next(x);
        }
      });
  }
  addCategorySpecification(data: any) {
    let categorySpecification = new CategorySpecification();
    categorySpecification.categoryId = this.categoryId;
    categorySpecification.specificationAttributeteId = data.id;
    this.categorySpecificationService
      .createCategorySpecification(categorySpecification)
      .then((x) => {
        this.categorySpecGrid.refreshGrid();
        this.addCategorySpecificationGrid.refresh();
      });
  }
  deletedCategoySpec() {
    this.addCategorySpecificationGrid.refresh();
  }
}
