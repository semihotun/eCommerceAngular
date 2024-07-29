import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import {
  IonContent,
  IonHeader,
  IonTitle,
  IonToolbar,
  IonIcon,
} from '@ionic/angular/standalone';
import { HeaderComponent } from 'src/app/uı/header/header.component';
import { GridComponent } from 'src/app/uı/grid/grid.component';

@Component({
  selector: 'app-specificationattributelist',
  templateUrl: './specificationattributelist.page.html',
  styleUrls: ['./specificationattributelist.page.scss'],
  standalone: true,
  imports: [
    IonIcon,
    IonContent,
    IonHeader,
    IonTitle,
    IonToolbar,
    CommonModule,
    FormsModule,
    HeaderComponent,
    GridComponent,
  ],
})
export class SpecificationattributelistPage implements OnInit {
  constructor() {}

  ngOnInit() {}
}
