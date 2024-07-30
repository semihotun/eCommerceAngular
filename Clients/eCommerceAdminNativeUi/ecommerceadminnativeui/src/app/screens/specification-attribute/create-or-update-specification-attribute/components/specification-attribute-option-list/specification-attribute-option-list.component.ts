import { Component, inject, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import {
  IonContent,
  IonHeader,
  IonTitle,
  IonToolbar,
} from '@ionic/angular/standalone';
import { GridComponent } from 'src/app/uı/grid/grid.component';
import { SpecificationAttributeStore } from './../../../../../stores/specificationattribute.store';
import { GridStore } from 'src/app/stores/grid.store';
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
export class SpecificationAttributeOptionListComponent implements OnInit {
  @Input() specificationAttributeId!: string;
  specificationAttributeStore = inject(SpecificationAttributeStore);
  gridStore = inject(GridStore);
  constructor() {}

  getAllData() {
    this.gridStore.setData;
  }
  ngOnInit() {}
}
