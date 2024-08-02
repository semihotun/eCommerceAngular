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
import { GridComponent } from 'src/app/uı/grid/grid.component';
import { HeaderComponent } from 'src/app/uı/header/header.component';

@Component({
  selector: 'app-page-list',
  templateUrl: './page-list.page.html',
  styleUrls: ['./page-list.page.scss'],
  standalone: true,
  imports: [
    IonContent,
    IonHeader,
    IonTitle,
    IonToolbar,
    CommonModule,
    FormsModule,
    GridComponent,
    HeaderComponent,
    IonIcon,
  ],
})
export class PageListPage implements OnInit {
  constructor() {}

  ngOnInit() {}
}
