import {Component, Input, OnInit} from '@angular/core';
import {faAngleLeft, faDatabase} from "@fortawesome/free-solid-svg-icons";

@Component({
  selector: 'app-action-item',
  templateUrl: './action-item.component.html',
  styleUrls: ['./action-item.component.scss']
})
export class ActionItemComponent implements OnInit {

  @Input() shouldShowArrowAtTheEnd = false;
  faDatabase = faDatabase;
  faAngleLeft = faAngleLeft;

  constructor() { }

  ngOnInit(): void {
  }

}
