import { Component, inject, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { GlobalService } from 'src/app/services/core/global.service';
import { NavController } from '@ionic/angular/standalone';
@Component({
  selector: 'app-sub-header',
  templateUrl: './sub-header.component.html',
  styleUrls: ['./sub-header.component.scss'],
  standalone: true,
  imports: [CommonModule, FormsModule],
})
export class SubHeaderComponent implements OnInit {
  @Input() previousUrl: string | null = null;
  @Input() title: string = '';
  router = inject(Router);
  glb = inject(GlobalService);
  navController = inject(NavController);
  constructor() {}

  routerNavigate() {
    if (this.previousUrl == '') {
      this.navController.navigateForward(this.previousUrl!);
    } else {
      this.navController.navigateBack(this.previousUrl!);
    }
  }
  ngOnInit() {}
}
