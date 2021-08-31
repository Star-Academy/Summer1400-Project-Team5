import {Component, Input, OnInit} from '@angular/core';
import {faAngleLeft, faDatabase} from "@fortawesome/free-solid-svg-icons";
import {MatDialog, MatDialogConfig} from "@angular/material/dialog";
import {DataType} from "../../models/data";
import {CSVDataDetailsDialogComponent} from "../../dialogs/data/csv-server-data-details-dialog/c-s-v-data-details-dialog.component";
import {SQLServerDataDetailsDialogComponent} from "../../dialogs/data/sql-data-details-dialog/s-q-l-server-data-details-dialog.component";
import {EditActionDialogComponent} from "../../dialogs/pipe/edit-action-dialog/edit-action-dialog.component";

@Component({
  selector: 'app-action-item',
  templateUrl: './action-item.component.html',
  styleUrls: ['./action-item.component.scss']
})
export class ActionItemComponent implements OnInit {

  @Input() shouldShowArrowAtTheEnd = false;
  faDatabase = faDatabase;
  faAngleLeft = faAngleLeft;

  constructor(private dialog: MatDialog) { }

  ngOnInit(): void {
  }

  openActionEditDialog(): void {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.autoFocus = true;
    // dialogConfig.data = action;
    this.dialog.open(EditActionDialogComponent, dialogConfig);
  }
}
