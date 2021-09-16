import { Component, Inject, Input, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { faCogs } from '@fortawesome/free-solid-svg-icons';
import action, { AggregateActionConfig, CalculateActionConfig, CalculateType, FilterActionConfig } from 'src/app/models/action';
import ActionItem, { ActionType, JoinActionConfig } from 'src/app/models/action';

@Component({
  selector: 'app-edit-action-dialog',
  templateUrl: './edit-action-dialog.component.html',
  styleUrls: ['./edit-action-dialog.component.scss', '../../../styles/base/dialogs.scss']
})
export class EditActionDialogComponent implements OnInit {
  faCogs = faCogs;



  selectedType = "جمع‌آوری"; 
  // جمع‌آوری - فیلتر - الحاق - محاسبه

  filter_columnName = "";
  filter_columnEqual = "";
  filter_columnMore = "";
  filter_columnLess = "";

  join_selectedData = "محصول تولیدکنندگان"

  aggregate_groupColumn = "";
  aggregate_addColumn = "";

  calculate_firstOperand = "";
  calculate_secondOperand = "";
  calculate_type = "";

  constructor(@Inject(MAT_DIALOG_DATA) public action: ActionItem) { }

  convertEnumToPersianOperator(type: CalculateType): string {
    switch (type) {
      case CalculateType.add: return "جمع";
      case CalculateType.subtract: return "تفریق";
      case CalculateType.multiply: return "ضرب";
      case CalculateType.divide: return "تقسیم";
      default: return "جمع";
    }
  }

  convertPersianToEnumOperator(persian: string): CalculateType {
    switch (persian) {
      case "جمع": return CalculateType.add;
      case "تفریق": return CalculateType.subtract;
      case "ضرب": return CalculateType.multiply;
      case "تقسیم": return CalculateType.divide;
      default: return CalculateType.add;
    }
  }

  ngOnInit(): void {
    switch (this.action.type) {
      case ActionType.join:
        this.selectedType = "الحاق";
        break;
      case ActionType.aggregate:
        this.selectedType = "جمع‌آوری";
        let aggregateConfig = this.action.config as AggregateActionConfig;
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
      case ActionType.calculate:
        this.selectedType = "محاسبه";
        let calculateConfig = this.action.config as CalculateActionConfig;
        this.calculate_firstOperand = calculateConfig.firstOperand;
        this.calculate_secondOperand = calculateConfig.secondOperand;
        this.calculate_type = this.convertEnumToPersianOperator(calculateConfig.type);
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
      case "محاسبه":
        this.action.type = ActionType.calculate;
        let c = new CalculateActionConfig();
        c.firstOperand = this.calculate_firstOperand;
        c.secondOperand = this.calculate_secondOperand;
        c.type = this.convertPersianToEnumOperator(this.calculate_type)
        this.action.config = c;
        break;
      
      default:
        break;
    }
  }

}
