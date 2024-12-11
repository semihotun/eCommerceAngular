import { CommonModule } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';
import { LazyImgDirective } from 'src/app/directives/lazy-img.directive';
import { AllShowcaseDTO } from 'src/app/models/responseModel/allShowcaseDTO';
import { GlobalService } from 'src/app/services/core/global.service';
import { StarRatingComponent } from '../../../../../../u\u0131/star-rating/star-rating.component';

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
  ],
})
export class ShowcaselistComponent implements OnInit {
  @Input() data!: AllShowcaseDTO;
  constructor(public glb: GlobalService) {}
  ngOnInit() {}
}
