import { Component, OnInit } from '@angular/core';
import {
  IonHeader,
  IonToolbar,
  IonTitle,
  IonContent,
  IonRouterOutlet,
} from '@ionic/angular/standalone';
import { GlobalService } from 'src/app/services/core/global.service';
import { FooterComponent } from 'src/app/uı/footer/footer.component';
import { HeaderComponent } from 'src/app/uı/header/header.component';
import { MobileFooterComponent } from 'src/app/uı/mobile-footer/mobile-footer.component';
import { SliderComponent } from './components/slider/slider.component';
import { ShowcaseComponent } from './components/showcase/showcase.component';
import { HomeService } from 'src/app/services/home.service';

@Component({
  selector: 'app-home',
  templateUrl: 'home.page.html',
  styleUrls: ['home.page.scss'],
  standalone: true,
  imports: [
    IonContent,
    HeaderComponent,
    FooterComponent,
    MobileFooterComponent,
    SliderComponent,
    ShowcaseComponent,
  ],
})
export class HomePage {
  constructor(public glb: GlobalService, private homeService: HomeService) {}
  ionViewWillEnter(): void {
    this.homeService.getHomeDTO();
  }
}
