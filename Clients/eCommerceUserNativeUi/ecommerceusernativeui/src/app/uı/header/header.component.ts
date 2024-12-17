import { CommonModule } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';

import { RouterModule } from '@angular/router';
import { GlobalService } from 'src/app/services/core/global.service';
import { TranslateModule } from '@ngx-translate/core';
import { ReactiveFormsModule, Validators } from '@angular/forms';
import { HeaderWebComponent } from './components/header-web/header-web.component';
import { HeaderMobileComponent } from './components/header-mobile/header-mobile.component';
import { IonContent } from '@ionic/angular/standalone';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
  standalone: true,
  imports: [
    IonContent,
    CommonModule,
    RouterModule,
    TranslateModule,
    ReactiveFormsModule,
    HeaderWebComponent,
    HeaderMobileComponent,
  ],
})
export class HeaderComponent implements OnInit {
  @Input() isNavigationBackUrl: string | null = null;
  constructor(public glb: GlobalService) {}
  ngOnInit(): void {
    console.log(this.glb.isMobil.value);
  }
}
