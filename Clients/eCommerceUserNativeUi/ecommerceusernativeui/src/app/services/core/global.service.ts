import { Injectable } from '@angular/core';
import { BehaviorSubject, fromEvent } from 'rxjs';
import { NavController } from '@ionic/angular/standalone';
import { environment } from 'src/environments/environment';
@Injectable({
  providedIn: 'root',
})
export class GlobalService {
  headerMobilWidth: number = 1000;
  isMobil: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  isShowMobilBars: BehaviorSubject<boolean | null> = new BehaviorSubject<
    boolean | null
  >(null);
  constructor(private navController: NavController) {
    this.onResize();
  }
  onResize() {
    fromEvent(window, 'resize').subscribe(() => {
      if (window.innerWidth > this.headerMobilWidth) {
        this.isMobil.next(false);
        this.isShowMobilBars.next(false);
      } else {
        this.isShowMobilBars.next(null);
        this.isMobil.next(true);
      }
    });
  }
  navigateProductDetail(slug: String): void {
    this.navController.navigateForward(['', slug]);
  }
  getImagePath(path: string) {
    if (path) {
      if (path.startsWith('http://') || path.startsWith('https://')) {
        return path;
      }
      return environment.photoPath + path;
    }
    return path;
  }
}
