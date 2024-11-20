import { Component, Input, OnInit } from '@angular/core';
import { AllShowcaseDTO } from 'src/app/models/responseModel/allShowcaseDTO';
import { Showcase } from 'src/app/models/responseModel/Showcase';

@Component({
  selector: 'app-showcase-text',
  templateUrl: './showcase-text.component.html',
  standalone: true,
  styleUrls: ['./showcase-text.component.scss'],
})
export class ShowcaseTextComponent implements OnInit {
  @Input() data!: AllShowcaseDTO;
  constructor() {}

  ngOnInit() {}
}
