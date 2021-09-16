import { Component, Inject, Input, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { faCogs } from '@fortawesome/free-solid-svg-icons';
import action, { AggregateActionConfig, FilterActionConfig } from 'src/app/models/action';
import ActionItem, { ActionType, JoinActionConfig } from 'src/app/models/action';

@Component({
  selector: 'app-edit-action-dialog',
  templateUrl: './edit-action-dialog.component.html',
  styleUrls: ['./edit-action-dialog.component.scss', '../../../styles/base/dialogs.scss']
})
export class EditActionDialogComponent implements OnInit {
  faCogs = faCogs;



  selectedType = "جمع‌آوری"; 
  // جمع‌آوری - فیلتر - الحاق

  filter_columnName = "";
  filter_columnEqual = "";
  filter_columnMore = "";
  filter_columnLess = "";

  join_selectedData = "محصول تولیدکنندگان"

  aggregate_groupColumn = "";
  aggregate_addColumn = "";

  constructor(@Inject(MAT_DIALOG_DATA) public action: ActionItem) { }

  ngOnInit(): void {
    switch (this.action.type) {
      case ActionType.join:
        this.selectedType = "الحاق";
        break;
      case ActionType.aggregate:
        this.selectedType = "جمع‌آوری";
        let aggregateConfig = this.action.config as AggregateActionConfig;
        console.log(aggregateConfig);
        this.aggregate_groupColumn = aggregateConfig.groupColumn;
        this.aggregate_addColumn = aggregateConfig.addColumn;
        break;
      case ActionType.filter:
        this.selectedType = "فیلتر";
        let filterConfig = this.action.config as FilterActionConfig;
        this.filter_columnName = filterConfig.columnName;
        this.filter_columnEqual = filterConfig.columnEqual;
        this.filter_columnLess = filterConfig.columnLess;
        this.filter_columnMore = filterConfig.columnMore;
        break;
      default:
        this.selectedType = "الحاق";
        break;
    }
  }

  submitTapped(): void {
    switch (this.selectedType) {
      case "الحاق":
        this.action.type = ActionType.join;
        this.action.config = new JoinActionConfig();
        break;
      case "جمع‌آوری":
        this.action.type = ActionType.aggregate;
        let a = new AggregateActionConfig();
        a.addColumn = this.aggregate_addColumn;
        a.groupColumn = this.aggregate_groupColumn;
        this.action.config = a;
        break;
      case "فیلتر":
        this.action.type = ActionType.filter;
        let f = new FilterActionConfig();
        f.columnEqual = this.filter_columnEqual;
        f.columnName = this.filter_columnName;
        f.columnLess = this.filter_columnLess;
        f.columnMore = this.filter_columnMore;
        this.action.config = f;

        break;
      
      default:
        break;
    }
  }

}
