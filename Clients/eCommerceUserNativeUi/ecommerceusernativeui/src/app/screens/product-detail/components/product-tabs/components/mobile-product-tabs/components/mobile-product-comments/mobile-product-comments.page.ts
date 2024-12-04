import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { IonContent, IonHeader, IonTitle, IonToolbar } from '@ionic/angular/standalone';

@Component({
  selector: 'app-mobile-product-comments',
  templateUrl: './mobile-product-comments.page.html',
  styleUrls: ['./mobile-product-comments.page.scss'],
  standalone: true,
  imports: [IonContent, IonHeader, IonTitle, IonToolbar, CommonModule, FormsModule]
})
export class MobileProductCommentsPage implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
