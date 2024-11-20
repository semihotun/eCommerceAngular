import { CommonModule } from '@angular/common';
import { Component, inject, Input, OnInit, ViewChild } from '@angular/core';
import {
  IonIcon,
  IonHeader,
  IonMenu,
  IonToolbar,
  IonButtons,
  IonTitle,
  IonContent,
  IonMenuButton,
  NavController,
  IonItem,
  IonList,
  IonPopover,
  IonButton,
} from '@ionic/angular/standalone';
import { RouterModule } from '@angular/router';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { LanguageList } from 'src/app/models/consts/languagelist';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
  standalone: true,
  imports: [
    IonContent,
    IonTitle,
    IonButtons,
    CommonModule,
    RouterModule,
    TranslateModule,
    IonMenu,
    IonHeader,
    IonToolbar,
    IonMenuButton,
    RouterModule,
  ],
})
export class HeaderComponent implements OnInit {
  navCtrl = inject(NavController);
  initRoute: string = '';
  @Input() title: string = '';
  @Input() backButton: boolean = false;
  translateService = inject(TranslateService);
  languages = LanguageList;
  isAuth: boolean = false;
  @ViewChild('menu') menu!: IonMenu;
  ngOnInit(): void {
    this.isAuthCheck();
  }
  routerNavigate() {
    this.navCtrl.back();
  }
  selectLanguage(languageCode: string) {
    this.translateService.use(languageCode);
    localStorage.setItem('languageCode', languageCode);
    this.menu.close();
    window.location.reload();
  }
  showSubNodeCategory(e: EventTarget) {
    const target = (e as HTMLElement).nextElementSibling as HTMLElement;
    if (!target.classList.contains('sub-category-close')) {
      target.classList.replace('sub-category-open', 'sub-category-close');
    } else if (target.classList.contains('sub-category-close')) {
      target.classList.replace('sub-category-close', 'sub-category-open');
    }
  }
  isAuthCheck() {
    const token = localStorage.getItem('token');
    this.isAuth = !!token;
  }
  logout() {
    localStorage.removeItem('token');
  }
}
