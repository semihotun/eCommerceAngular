import { Component, OnInit } from '@angular/core';
import { HeaderComponent } from 'src/app/uı/header/header.component';
import { GridComponent } from 'src/app/uı/grid/grid.component';
import { IonIcon } from '@ionic/angular/standalone';
@Component({
  selector: 'app-showcase-list',
  templateUrl: './showcase-list.page.html',
  styleUrls: ['./showcase-list.page.scss'],
  standalone: true,
  imports: [HeaderComponent, GridComponent, IonIcon],
})
export class ShowcaseListPage implements OnInit {
  constructor() {}

  ngOnInit() {}
}
