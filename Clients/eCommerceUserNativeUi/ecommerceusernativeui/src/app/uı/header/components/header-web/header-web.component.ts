import { CommonModule } from '@angular/common';
import {
  ChangeDetectorRef,
  Component,
  ElementRef,
  HostListener,
  Input,
  OnInit,
} from '@angular/core';
import { GestureController } from '@ionic/angular';
import { BehaviorSubject, Subject } from 'rxjs';
import { NavController } from '@ionic/angular/standalone';
import { GlobalService } from 'src/app/services/core/global.service';
import { CustomerUserService } from 'src/app/services/customer-user.service';
import { TranslateModule } from '@ngx-translate/core';
import { HomeStore } from 'src/app/stores/home.store';
import { FormControl, ReactiveFormsModule, Validators } from '@angular/forms';
import { HomeService } from 'src/app/services/home.service';
@Component({
  selector: 'app-header-web',
  templateUrl: './header-web.component.html',
  styleUrls: ['./header-web.component.scss'],
  standalone: true,
  imports: [CommonModule, TranslateModule, ReactiveFormsModule],
})
export class HeaderWebComponent implements OnInit {
  headerMobilWidth: number = 1000;
  headerBottom: BehaviorSubject<string> = new BehaviorSubject<string>('');
  @Input() isNavigationBackUrl: string | null = null;
  onDestroy = new Subject<void>();
  isSearchShow: boolean = false;
  searchText = new FormControl('', [
    Validators.required,
    Validators.minLength(3),
  ]);
  ngOnDestroy(): void {
    this.onDestroy.unsubscribe();
    this.glb.isShowMobilBars.next(null);
  }
  constructor(
    private elRef: ElementRef<HTMLElement>,
    public glb: GlobalService,
    public userService: CustomerUserService,
    public homeStore: HomeStore,
    public navController: NavController,
    private homeService: HomeService
  ) {}
  ngOnInit(): void {
    this.homeService.getCategoryTree();
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
  changeSearchInput() {
    this.isSearchShow = false;
    if (this.searchText.valid) {
      this.isSearchShow = true;
      this.homeService.getHomeProductSearch(this.searchText.value!);
    }
  }
  @HostListener('document:click', ['$event'])
  onDocumentClick(event: MouseEvent): void {
    const searchWrapper = document.querySelector('.search-input-wrapper');
    if (searchWrapper && !searchWrapper.contains(event.target as Node)) {
      this.isSearchShow = false;
    }
  }
  clickSearchInput() {
    if (this.glb.isMobil.value) {
      this.glb.isMobilSearch.next(true);
    }
  }
}
