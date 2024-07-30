import {
  Component,
  inject,
  Input,
  OnInit,
  WritableSignal,
  signal,
  OnDestroy,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import {
  IonContent,
  IonHeader,
  IonTitle,
  IonToolbar,
} from '@ionic/angular/standalone';
import { GridComponent } from 'src/app/uı/grid/grid.component';

import { GridStore } from 'src/app/stores/grid.store';
import { SpecificationAttributeOptionService } from 'src/app/services/specification-attribute/specification-attribute-option.service';
import { GridPostData } from 'src/app/models/core/grid';
import { ActivatedRoute } from '@angular/router';
import { BehaviorSubject, Subject, takeUntil } from 'rxjs';
import { SpecificationAttributeStore } from 'src/app/stores/specificationattribute.store';
@Component({
  selector: 'app-specification-attribute-option-list',
  templateUrl: './specification-attribute-option-list.component.html',
  styleUrls: ['./specification-attribute-option-list.component.scss'],
  imports: [
    IonContent,
    IonHeader,
    IonTitle,
    IonToolbar,
    CommonModule,
    FormsModule,
    GridComponent,
  ],
  standalone: true,
})
export class SpecificationAttributeOptionListComponent
  implements OnInit, OnDestroy
{
  gridStore = inject(SpecificationAttributeStore);
  data: BehaviorSubject<any> = new BehaviorSubject<any>(null);
  onDestroy: Subject<void> = new Subject<void>();
  constructor(
    private activatedRoute: ActivatedRoute,
    private specificationAttributeOptionService: SpecificationAttributeOptionService,
    private specificationAttributeStore: SpecificationAttributeStore
  ) {}
  ngOnDestroy(): void {
    this.onDestroy.next();
    this.onDestroy.complete();
  }

  async getAllData(data?: GridPostData) {
    this.activatedRoute.paramMap.subscribe(async (params) => {
      await this.specificationAttributeOptionService.getSpecificationOptionAttributeGrid(
        { ...data, specificationAttributeId: params.get('id') }
      );
    });
  }
  async ngOnInit() {
    this.specificationAttributeStore.specificationAttributeOptionGridObservable$
      .pipe(takeUntil(this.onDestroy))
      .subscribe((x) => {
        if (x) {
          this.data.next(x);
        }
      });
  }
}
