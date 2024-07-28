import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import {
  IonContent,
  IonHeader,
  IonTitle,
  IonToolbar,
} from '@ionic/angular/standalone';
import { HeaderComponent } from '../../u\u0131/header/header.component';
import { TranslateModule } from '@ngx-translate/core';
import { GridComponent } from 'src/app/uı/grid/grid.component';
@Component({
  selector: 'app-brand',
  templateUrl: './brand.page.html',
  styleUrls: ['./brand.page.scss'],
  standalone: true,
  imports: [
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
export class BrandPage implements OnInit {
  constructor() {}

  ngOnInit() {}
}
