import { Component, OnInit } from '@angular/core';
import { IonApp, IonContent } from '@ionic/angular/standalone';
import { RouterModule } from '@angular/router';
import { register } from 'swiper/element/bundle';
import {
  TranslateModule,
  TranslatePipe,
  TranslateService,
} from '@ngx-translate/core';
import { SpinnerComponent } from './u\u0131/spinner/spinner.component';
import { HomeService } from './services/home.service';
register();
@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html',
  standalone: true,
  imports: [
    IonContent,
    IonApp,
    RouterModule,
    TranslateModule,
    SpinnerComponent,
  ],
})
export class AppComponent implements OnInit {
  constructor(
    private translate: TranslateService,
    private homeService: HomeService
  ) {
    this.translate.setDefaultLang('tr');
  }
  ngOnInit() {
    this.homeService.getWebsiteInfo();
  }
}
