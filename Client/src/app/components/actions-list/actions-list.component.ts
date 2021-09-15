import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Action } from 'rxjs/internal/scheduler/Action';
import ActionItem, { ActionConfig, ActionType } from 'src/app/models/action';

@Component({
  selector: 'app-actions-list',
  templateUrl: './actions-list.component.html',
  styleUrls: ['./actions-list.component.scss']
})
export class ActionsListComponent implements OnInit {

  constructor() { }

  // rawActions: ActionItem[] = [
  //   new ActionItem(ActionType.join, new ActionConfig()),
  //   new ActionItem(ActionType.aggregate, new ActionConfig()),
  //   new ActionItem(ActionType.join, new ActionConfig()),
  //   new ActionItem(ActionType.filter, new ActionConfig()),
  // ];

  rawActions: ActionItem[] = [];

  @Input()
  get actions() {
    return this.rawActions;
  }

  @Output() actionsChanges = new EventEmitter();

  set actions(value) {
    this.rawActions = value;
    this.actionsChanges.emit(this.rawActions);
  }

  ngOnInit(): void {
  }

}
