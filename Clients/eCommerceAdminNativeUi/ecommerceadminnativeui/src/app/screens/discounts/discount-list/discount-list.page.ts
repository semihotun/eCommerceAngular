import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { IonIcon } from '@ionic/angular/standalone';
import { TranslateModule } from '@ngx-translate/core';
import { GridComponent } from 'src/app/uı/grid/grid.component';
import { HeaderComponent } from 'src/app/uı/header/header.component';
@Component({
  selector: 'app-discount-list',
  templateUrl: './discount-list.page.html',
  styleUrls: ['./discount-list.page.scss'],
  standalone: true,
  imports: [
    IonIcon,
    CommonModule,
    FormsModule,
    HeaderComponent,
    TranslateModule,
    GridComponent,
  ],
})
export class DiscountListPage implements OnInit {
  constructor() {}

  ngOnInit() {}
}
