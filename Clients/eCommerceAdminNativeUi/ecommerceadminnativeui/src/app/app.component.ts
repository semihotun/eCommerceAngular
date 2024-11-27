import { Component } from '@angular/core';
import { IonApp, IonContent, NavController } from '@ionic/angular/standalone';
import { RouterModule } from '@angular/router';
import { register } from 'swiper/element/bundle';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { SpinnerComponent } from './u\u0131/spinner/spinner.component';
import { Platform } from '@ionic/angular/standalone';
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
export class AppComponent {
  constructor(
    private translate: TranslateService,
    private platform: Platform,
    private navController: NavController
  ) {
    this.translate.use(
      localStorage.getItem('languageCode')?.toString()! ?? 'tr'
    );
    localStorage.setItem(
      'languageCode',
      localStorage.getItem('languageCode')?.toString()! ?? 'tr'
    );
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
}
