import { Component, Input, OnInit } from '@angular/core';
import { TranslateModule } from '@ngx-translate/core';
import { AllShowcaseDTO } from 'src/app/models/responseModel/allShowcaseDTO';
import { Showcase } from 'src/app/models/responseModel/Showcase';

@Component({
  selector: 'app-showcaselist',
  templateUrl: './showcaselist.component.html',
  styleUrls: ['./showcaselist.component.scss'],
  standalone: true,
  imports: [TranslateModule],
})
export class ShowcaselistComponent implements OnInit {
  @Input() data!: AllShowcaseDTO;
  constructor() {}
  ngOnInit() {}
}
