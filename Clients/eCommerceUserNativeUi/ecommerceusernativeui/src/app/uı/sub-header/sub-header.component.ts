import {
  Component,
  ContentChild,
  ElementRef,
  EventEmitter,
  inject,
  Input,
  OnInit,
  Output,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { GlobalService } from 'src/app/services/core/global.service';
import { NavController } from '@ionic/angular/standalone';

@Component({
  selector: 'app-sub-header',
  templateUrl: './sub-header.component.html',
  styleUrls: ['./sub-header.component.scss'],
  standalone: true,
  imports: [CommonModule],
})
export class SubHeaderComponent implements OnInit {
  @ContentChild('customButton', { static: false })
  customButton!: ElementRef<HTMLButtonElement>;
  @Input() previousUrl: string | null = null;
  @Input() title: string = '';
  router = inject(Router);
  glb = inject(GlobalService);
  navController = inject(NavController);

  constructor() {}

  ngOnInit() {}
  ngAfterViewInit() {
    if (this.customButton) {
      this.customButton.nativeElement.addEventListener('click', () => {});
    }
  }

  routerNavigate() {
    this.navController.navigateBack(this.previousUrl!);
  }
}
