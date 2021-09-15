import { Component, Inject, Input, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { faCogs } from '@fortawesome/free-solid-svg-icons';
import action from 'src/app/models/action';
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

  constructor(@Inject(MAT_DIALOG_DATA) public action: ActionItem) { }

  ngOnInit(): void {
    console.log(this.action.type);
    switch (this.action.type) {
      case ActionType.join:
        this.selectedType = "الحاق";
        break;
      case ActionType.aggregate:
        this.selectedType = "جمع‌آوری";
        break;
      case ActionType.filter:
        this.selectedType = "فیلتر";
        break;
      default:
        this.selectedType = "الحاق";
        break;
    }
  }

  submitTapped(): void {
    alert(this.selectedType);
  }

}
