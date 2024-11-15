import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import {
  IonContent,
  IonHeader,
  IonTitle,
  IonToolbar,
} from '@ionic/angular/standalone';
import { HeaderComponent } from 'src/app/uı/header/header.component';
import { TranslateModule } from '@ngx-translate/core';
import { GridComponent } from 'src/app/uı/grid/grid.component';
import { IonIcon } from '@ionic/angular/standalone';
@Component({
  selector: 'app-user-group-list',
  templateUrl: './user-group-list.page.html',
  styleUrls: ['./user-group-list.page.scss'],
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
export class UserGroupListPage implements OnInit {
  constructor() {}

  ngOnInit() {}
}
