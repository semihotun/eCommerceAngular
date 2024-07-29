import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-btn-submit',
  templateUrl: './btn-submit.component.html',
  styleUrls: ['./btn-submit.component.scss'],
  standalone: true,
})
export class BtnSubmitComponent implements OnInit {
  constructor() {}
  @Input() class: string = '';
  @Input() text: string = '';
  ngOnInit() {}
}
