import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { GlobalService } from 'src/app/services/core/global.service';
import { IonIcon } from '@ionic/angular/standalone';
import { RouterModule } from '@angular/router';
import { CustomerUserService } from 'src/app/services/customer-user.service';

@Component({
  selector: 'app-mobile-footer',
  templateUrl: './mobile-footer.component.html',
  styleUrls: ['./mobile-footer.component.scss'],
  standalone: true,
  imports: [IonIcon, CommonModule, RouterModule],
})
export class MobileFooterComponent implements OnInit {
  constructor(
    public glb: GlobalService,
    public userService: CustomerUserService
  ) {}

  ngOnInit() {}
}
