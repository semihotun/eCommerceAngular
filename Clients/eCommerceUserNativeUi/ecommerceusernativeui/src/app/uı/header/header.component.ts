import { CommonModule } from '@angular/common';
import {
  ChangeDetectorRef,
  Component,
  ElementRef,
  HostListener,
  Input,
  OnInit,
  ViewChild,
  inject,
} from '@angular/core';
import { Gesture, GestureController, GestureDetail } from '@ionic/angular';
import { BehaviorSubject, Subject, takeUntil } from 'rxjs';
import { NavController } from '@ionic/angular/standalone';
import { Router, RouterModule } from '@angular/router';
import { GlobalService } from 'src/app/services/core/global.service';
import { CustomerUserService } from 'src/app/services/customer-user.service';
import { TranslateModule } from '@ngx-translate/core';
import { HomeStore } from 'src/app/stores/home.store';
import { HomeService } from './../../services/home.service';
@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
  standalone: true,
  imports: [CommonModule, RouterModule, TranslateModule],
})
export class HeaderComponent implements OnInit {
  subCategoryLists!: NodeListOf<HTMLElement>;
  headerMobilWidth: number = 1000;
  headerBottom: BehaviorSubject<string> = new BehaviorSubject<string>('');
  @Input() isNavigationBackUrl: string | null = null;
  onDestroy = new Subject<void>();
  router = inject(Router);
  ngOnDestroy(): void {
    this.onDestroy.unsubscribe();
    this.glb.isShowMobilBars.next(null);
  }
  constructor(
    private elRef: ElementRef<HTMLElement>,
    private gestureCtrl: GestureController,
    private cdr: ChangeDetectorRef,
    public glb: GlobalService,
    public userService: CustomerUserService,
    public homeStore: HomeStore,
    public navController: NavController,
    private homeService: HomeService
  ) {}
  ngOnInit(): void {
    this.initializeGesture();
    this.changeHeaderRightClasses();
    if (window.innerWidth < this.headerMobilWidth) {
      this.glb.isMobil.next(true);
    }
    this.homeService.getCategoryTree();
  }
  @HostListener('document:click', ['$event'])
  handleDocumentClick(event: MouseEvent) {
    const clickedInside =
      this.headerRight.nativeElement.parentNode?.parentNode?.contains(
        event.target as Node
      );
    if (clickedInside == false && this.glb.isShowMobilBars.value == true) {
      this.removeSubCategoryOpenClass();
      this.glb.isShowMobilBars.next(false);
    }
  }
  removeSubCategoryOpenClass() {
    const elements =
      this.elRef.nativeElement.querySelectorAll('.sub-category-open');
    elements.forEach((element: Element) => {
      (element as HTMLElement).classList.replace(
        'sub-category-open',
        'sub-category-close'
      );
    });
  }

  @ViewChild('headerRight', { static: true })
  headerRight!: ElementRef<HTMLElement>;

  initializeGesture() {
    const gesture: Gesture = this.gestureCtrl.create({
      el: this.headerRight.nativeElement,
      gestureName: 'swipe-right',
      onMove: (ev) => this.onMove(ev),
      onEnd: (ev) => this.onEnd(ev),
    });
    gesture.enable(true);
  }
  onMove(ev: GestureDetail) {
    if (ev.deltaX > 0) {
      ///Burada ileride move oldukça header-right'ın left değeri arttırılabilir
    }
  }

  onEnd(ev: GestureDetail) {
    if (ev.deltaX > 0) {
      this.headerBottom.next('deactive');
      this.cdr.detectChanges();
    }
  }
  ngAfterViewInit(): void {
    this.subCategoryLists =
      this.elRef.nativeElement.querySelectorAll<HTMLElement>(
        '.sub-category-list'
      );
  }
  showMobileBars() {
    this.glb.isShowMobilBars.next(!this.glb.isShowMobilBars.value);
  }
  showSubNodeCategory(e: EventTarget) {
    const target = (e as HTMLElement).nextElementSibling as HTMLElement;
    if (!target.classList.contains('sub-category-close')) {
      target.classList.replace('sub-category-open', 'sub-category-close');
    } else if (target.classList.contains('sub-category-close')) {
      target.classList.replace('sub-category-close', 'sub-category-open');
    }
  }
  changeHeaderRightClasses() {
    if (window.innerWidth < this.headerMobilWidth) {
      this.glb.isShowMobilBars
        .pipe(takeUntil(this.onDestroy))
        .subscribe((isShowMobilBars) => {
          if (isShowMobilBars == true) {
            this.headerBottom.next('active');
            return;
          } else if (isShowMobilBars == false) {
            this.headerBottom.next('deactive');
            return;
          }
          this.headerBottom.next('display-none');
        });
    }
  }
  backButtonClick() {
    this.navController.navigateBack(this.isNavigationBackUrl!);
  }
}
