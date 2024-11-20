import { CommonModule } from '@angular/common';
import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { HomeStore } from 'src/app/stores/home.store';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  standalone: true,
  styleUrls: ['./footer.component.scss'],
  imports: [CommonModule],
  encapsulation: ViewEncapsulation.None,
})
export class FooterComponent implements OnInit {
  constructor(public homeStore: HomeStore) {}

  ngOnInit() {}
}
