import { Component} from '@angular/core';
import { faPlusSquare } from '@fortawesome/free-solid-svg-icons';
import {MatDialog, MatDialogConfig} from "@angular/material/dialog";
import {AddDataDialogComponent} from "../../dialogs/add-data-dialog/add-data-dialog.component";

@Component({
  selector: 'app-data-list-page',
  templateUrl: './data-list-page.component.html',
  styleUrls: ['./data-list-page.component.scss', '../../styles/base/responsive_table.scss']
})
export class DataListPageComponent {
  faPlusSquare = faPlusSquare;

  constructor(private dialog: MatDialog) {
  }

  addDataTapped() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    this.dialog.open(AddDataDialogComponent, dialogConfig);
  }
}
