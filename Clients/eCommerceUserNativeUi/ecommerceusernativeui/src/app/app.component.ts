import { Component, OnInit } from '@angular/core';
import { IonApp, IonContent, NavController } from '@ionic/angular/standalone';
import { RouterModule } from '@angular/router';
import { register } from 'swiper/element/bundle';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { Platform } from '@ionic/angular/standalone';

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
    private homeService: HomeService,
    private platform: Platform,
    private navController: NavController
  ) {
    this.translate.setDefaultLang('tr');
    this.platform
      .ready()
      .then(() => {
        this.platform.backButton.subscribeWithPriority(
          1,
          (processNextHandler) => {
            this.navController.back();
            processNextHandler();
          }
        );
      })
      .catch((error) => {
        console.error('Platform is not ready', error);
      });
  }
  ngOnInit() {
    this.homeService.getWebsiteInfo();
  }
}
