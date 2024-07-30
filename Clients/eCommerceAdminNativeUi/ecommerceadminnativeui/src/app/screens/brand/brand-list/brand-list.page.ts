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
import { TranslateModule } from '@ngx-translate/core';
import { GridComponent } from 'src/app/uı/grid/grid.component';
import { HeaderComponent } from 'src/app/uı/header/header.component';
@Component({
  selector: 'app-brand-list',
  templateUrl: './brand-list.page.html',
  styleUrls: ['./brand-list.page.scss'],
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
    TranslateModule,
    GridComponent,
  ],
})
export class BrandListPage implements OnInit {
  constructor() {}

  ngOnInit() {}
}
