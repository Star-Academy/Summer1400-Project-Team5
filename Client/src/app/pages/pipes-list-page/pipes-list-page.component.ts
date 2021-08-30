import { Component} from '@angular/core';
import { faPlusSquare } from '@fortawesome/free-solid-svg-icons';
import {MatDialog, MatDialogConfig} from "@angular/material/dialog";
import {AddDataDialogComponent} from "../../dialogs/data/add-data-dialog/add-data-dialog.component";
import {AddPipeDialogComponent} from "../../dialogs/pipe/add-pipe-dialog/add-pipe-dialog.component";

@Component({
  selector: 'app-pipes-list-page',
  templateUrl: './pipes-list-page.component.html',
  styleUrls: ['./pipes-list-page.component.scss', '../../styles/base/responsive_table.scss']
})
export class PipesListPageComponent {
  faPlusSquare = faPlusSquare;

  constructor(private dialog: MatDialog) {
  }

  addPipeTapped() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.autoFocus = true;
    this.dialog.open(AddPipeDialogComponent, dialogConfig);
  }
}
