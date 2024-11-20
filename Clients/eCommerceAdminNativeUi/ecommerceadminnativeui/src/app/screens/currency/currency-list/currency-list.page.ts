import { Component, OnInit } from '@angular/core';
import { IonIcon } from '@ionic/angular/standalone';
import { GridComponent } from 'src/app/uı/grid/grid.component';
import { HeaderComponent } from 'src/app/uı/header/header.component';

@Component({
  selector: 'app-currency-list',
  templateUrl: './currency-list.page.html',
  styleUrls: ['./currency-list.page.scss'],
  standalone: true,
  imports: [GridComponent, IonIcon, HeaderComponent],
})
export class CurrencyListPage implements OnInit {
  constructor() {}

  ngOnInit() {}
}
