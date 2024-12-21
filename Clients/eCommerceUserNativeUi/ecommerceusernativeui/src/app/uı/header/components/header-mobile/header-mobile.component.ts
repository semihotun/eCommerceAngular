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
import { Keyboard } from '@capacitor/keyboard';
import { Gesture, GestureController } from '@ionic/angular';
import { BehaviorSubject, Subject } from 'rxjs';
import { NavController } from '@ionic/angular/standalone';
import { Router, RouterModule } from '@angular/router';
import { GlobalService } from 'src/app/services/core/global.service';
import { CustomerUserService } from 'src/app/services/customer-user.service';
import { TranslateModule } from '@ngx-translate/core';
import { HomeStore } from 'src/app/stores/home.store';
import { FormControl, ReactiveFormsModule, Validators } from '@angular/forms';
import { HomeService } from 'src/app/services/home.service';
@Component({
  selector: 'app-header-mobile',
  templateUrl: './header-mobile.component.html',
  styleUrls: ['./header-mobile.component.scss'],
  standalone: true,
  imports: [CommonModule, RouterModule, TranslateModule, ReactiveFormsModule],
})
export class HeaderMobileComponent implements OnInit {
  @ViewChild('headerRight', { static: true })
  headerRight!: ElementRef<HTMLElement>;
  headerMobilWidth: number = 1000;
  headerBottom: BehaviorSubject<string> = new BehaviorSubject<string>(
    'display-none'
  );
  @Input() isNavigationBackUrl: string | null = null;
  onDestroy = new Subject<void>();
  router = inject(Router);
  searchText = new FormControl('', [
    Validators.required,
    Validators.minLength(2),
  ]);
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
    this.homeService.getCategoryTree();
  }
  initializeGesture() {
    const gesture: Gesture = this.gestureCtrl.create({
      el: this.headerRight.nativeElement,
      gestureName: 'swipe-right',
      onEnd: (ev) => {
        if (ev.deltaX > 0) {
          this.headerBottom.next('deactive');
          this.cdr.detectChanges();
        }
      },
    });
    gesture.enable(true);
  }

  @HostListener('document:click', ['$event'])
  handleDocumentClick(event: MouseEvent) {
    const clickedInside =
      this.headerRight.nativeElement.parentNode?.parentNode?.contains(
        event.target as Node
      );
    if (clickedInside == false && this.glb.isShowMobilBars.value == true) {
      this.removeSubCategoryOpenClass();
      this.changeHeaderRightClasses(false);
    }
    if (this.glb.isMobilSearch.value) {
      const searchWrapper = document.querySelector('.search-input-wrapper');
      if (searchWrapper && !searchWrapper.contains(event.target as Node)) {
        this.searchClose();
      }
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
  openOrCloseMobileBars() {
    this.changeHeaderRightClasses(!this.glb.isShowMobilBars.value);
  }
  showSubNodeCategory(e: EventTarget) {
    const target = (e as HTMLElement).nextElementSibling as HTMLElement;
    if (!target.classList.contains('sub-category-close')) {
      target.classList.replace('sub-category-open', 'sub-category-close');
    } else if (target.classList.contains('sub-category-close')) {
      target.classList.replace('sub-category-close', 'sub-category-open');
    }
  }
  changeHeaderRightClasses(data: boolean) {
    this.glb.isShowMobilBars.next(data);
    if (this.glb.isShowMobilBars.value == true) {
      this.headerBottom.next('active');
      return;
    } else if (this.glb.isShowMobilBars.value == false) {
      this.headerBottom.next('deactive');
      return;
    } else {
      this.headerBottom.next('display-none');
    }
  }
  backButtonClick() {
    this.navController.back();
    // if (this.isNavigationBackUrl) {
    //   this.navController.navigateBack(this.isNavigationBackUrl!);
    // } else {
    //   this.navController.back();
    // }
  }
  changeSearchInput(event: Event) {
    if (this.searchText.valid) {
      this.homeService.getHomeProductSearch(this.searchText.value!);
    } else {
      this.homeStore.setHomeSearchList([]);
    }
  }

  searchClose() {
    Keyboard.hide();
    this.searchText.setValue('');
    this.glb.isMobilSearch.next(false);
  }
  clickSearchInput() {
    if (this.glb.isMobil.value) {
      this.glb.isMobilSearch.next(true);
    }
  }
  navigateTo(path: string): void {
    this.navController.navigateForward(path);
  }
}
