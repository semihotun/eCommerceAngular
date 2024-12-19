import {
  Component,
  EventEmitter,
  inject,
  Input,
  OnInit,
  Output,
  ViewChild,
} from '@angular/core';
import { TranslateModule } from '@ngx-translate/core';
import { BehaviorSubject, Subject, takeUntil } from 'rxjs';
import { GridPostData } from 'src/app/models/core/grid';
import { CategorySpecificationService } from 'src/app/services/category/category-specification.service';
import { CategorySpecificationStore } from 'src/app/stores/category-specification.store';
import { GridComponent } from 'src/app/uı/grid/grid.component';

@Component({
  selector: 'app-category-specification-grid',
  templateUrl: './category-specification-grid.component.html',
  styleUrls: ['./category-specification-grid.component.scss'],
  standalone: true,
  imports: [TranslateModule, GridComponent],
})
export class CategorySpecificationGridComponent implements OnInit {
  @Input('categoryId') categoryId!: string;
  categorySpecificationStore = inject(CategorySpecificationStore);
  data: BehaviorSubject<any> = new BehaviorSubject<any>(null);
  onDestroy: Subject<void> = new Subject<void>();
  categorySpecificationService = inject(CategorySpecificationService);
  @ViewChild('categorySpecGrid') categorySpecGrid!: GridComponent;
  @Output() deletedCategorySpec = new EventEmitter<boolean>();
  constructor() {}
  ngOnDestroy(): void {
    this.onDestroy.next();
    this.onDestroy.complete();
  }

  async getAllData(data?: GridPostData) {
    await this.categorySpecificationService.getAllSpecificationGridDTO(
      this.categoryId,
      data!
    );
  }
  async ngOnInit() {
    this.categorySpecificationStore.categorySpecGridObservable$
      .pipe(takeUntil(this.onDestroy))
      .subscribe((x) => {
        if (x) {
          this.data.next(x);
        }
      });
  }
  refreshGrid() {
    this.categorySpecGrid.refresh();
  }
  deleteCategorySpec(data: any) {
    this.categorySpecificationService
      .deleteCategorySpecificationAttribute(data.id)
      .then((x) => {
        this.categorySpecGrid.refresh();
        this.deletedCategorySpec.emit(true);
      });
  }
}
