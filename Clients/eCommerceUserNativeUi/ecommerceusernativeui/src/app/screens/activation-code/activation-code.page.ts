import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { UserService } from 'src/app/services/user.service';
import { ActivatedRoute } from '@angular/router';
import { ToastService } from 'src/app/services/core/toast.service';
import { NavController } from '@ionic/angular/standalone';
@Component({
  selector: 'app-activation-code',
  templateUrl: './activation-code.page.html',
  styleUrls: ['./activation-code.page.scss'],
  standalone: true,
  imports: [],
})
export class ActivationCodePage implements OnInit {
  constructor(
    private translateService: TranslateService,
    private userService: UserService,
    private activatedRoute: ActivatedRoute,
    private toastService: ToastService,
    private navController: NavController
  ) {}

  ngOnInit() {
    this.activatedRoute.paramMap.subscribe(async (params) => {
      const id = params.get('id');
      const token = localStorage.getItem('token');
      if (id != null && token != null) {
        this.userService.customerUserActivationConfirmation({
          ActivationCode: id,
        });
        this.navController.navigateRoot('');
      } else if (id != null && token == null) {
        this.toastService.presentDangerToast(
          this.translateService.instant('User Not Login In', 6000)
        );
        this.navController.navigateForward(['login', id!]);
      } else {
        this.toastService.presentDangerToast(
          this.translateService.instant('Activation Code Incorrect')
        );
        this.navController.navigateRoot('');
      }
    });
  }
}
