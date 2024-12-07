import { Injectable } from '@angular/core';
import { ToastController } from '@ionic/angular/standalone';
import { TranslateService } from '@ngx-translate/core';

@Injectable({
  providedIn: 'root',
})
export class ToastService {
  constructor(
    private toastController: ToastController,
    private translate: TranslateService
  ) {}

  async presentSuccessToast(
    message: string = 'Operation Completed',
    duration: number = 3000
  ) {
    const messageTranslated =
      this.translate.instant(message) ?? 'Operation Completed';
    const toast = await this.toastController.create({
      message: messageTranslated,
      duration: duration,
      color: 'success',
      position: 'bottom',
      cssClass: 'success-toast',
    });
    toast.present();
  }

  async presentDangerToast(
    message: string = 'Operation Failed',
    duration: number = 3000
  ) {
    const messageTranslated =
      this.translate.instant(message) ?? 'Operation Failed';
    const toast = await this.toastController.create({
      message: messageTranslated,
      duration: duration,
      color: 'danger',
      position: 'bottom',
      cssClass: 'danger-toast',
    });
    toast.present();
  }
}
