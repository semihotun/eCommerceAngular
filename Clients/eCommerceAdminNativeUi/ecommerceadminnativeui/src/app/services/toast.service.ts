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

  async presentSuccessToast(message: string = 'Operation Completed') {
    const messageTranslated = await this.translate.get(message).toPromise();
    const toast = await this.toastController.create({
      message: messageTranslated,
      duration: 3000,
      color: 'success',
      position: 'bottom',
      cssClass: 'success-toast',
    });
    toast.present();
  }

  async presentDangerToast(message: string = 'Operation Failed') {
    const messageTranslated = await this.translate.get(message).toPromise();
    const toast = await this.toastController.create({
      message: messageTranslated,
      duration: 3000,
      color: 'danger',
      position: 'bottom',
      cssClass: 'danger-toast',
    });
    toast.present();
  }
}
