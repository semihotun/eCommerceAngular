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
  selector: 'app-warehouse-list',
  templateUrl: './warehouse-list.page.html',
  styleUrls: ['./warehouse-list.page.scss'],
  standalone: true,
  imports: [CommonModule, GridComponent, HeaderComponent, IonIcon],
})
export class WarehouseListPage implements OnInit {
  constructor() {}

  ngOnInit() {}
}
