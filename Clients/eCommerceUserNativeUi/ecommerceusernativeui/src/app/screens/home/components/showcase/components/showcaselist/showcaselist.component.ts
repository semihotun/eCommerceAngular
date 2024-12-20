import { CommonModule } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';
import { LazyImgDirective } from 'src/app/directives/lazy-img.directive';
import { AllShowcaseDTO } from 'src/app/models/responseModel/allShowcaseDTO';
import { GlobalService } from 'src/app/services/core/global.service';
import { StarRatingComponent } from '../../../../../../u\u0131/star-rating/star-rating.component';
import { FavoriteProductButtonComponent } from '../../../../../../u\u0131/favorite-product-button/favorite-product-button.component';
import { NavController } from '@ionic/angular/standalone';
@Component({
  selector: 'app-showcaselist',
  templateUrl: './showcaselist.component.html',
  styleUrls: ['./showcaselist.component.scss'],
  standalone: true,
  imports: [
    TranslateModule,
    CommonModule,
    RouterModule,
    LazyImgDirective,
    StarRatingComponent,
    FavoriteProductButtonComponent,
  ],
})
export class ShowcaselistComponent implements OnInit {
  @Input() data!: AllShowcaseDTO;
  constructor(public glb: GlobalService, public navController: NavController) {}
  ngOnInit() {}
}
