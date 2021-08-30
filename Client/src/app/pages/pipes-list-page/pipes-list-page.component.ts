import { Component} from '@angular/core';
import { faPlus } from '@fortawesome/free-solid-svg-icons';
import {MatDialog, MatDialogConfig} from "@angular/material/dialog";
import {AddPipeDialogComponent} from "../../dialogs/pipe/add-pipe-dialog/add-pipe-dialog.component";
import Pipe from "../../models/pipe";

@Component({
  selector: 'app-pipes-list-page',
  templateUrl: './pipes-list-page.component.html',
  styleUrls: ['./pipes-list-page.component.scss', '../../styles/base/responsive_table.scss']
})
export class PipesListPageComponent {
  faPlus = faPlus;

  pipes: Pipe[] = [
    new Pipe(1, "لوله‌ی نخست"),
    new Pipe(2, "لوله‌ی دوم"),
    new Pipe(3, "لوله‌ی جدید"),
  ]

  constructor(private dialog: MatDialog) {
  }

  addPipeTapped() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.autoFocus = true;
    this.dialog.open(AddPipeDialogComponent, dialogConfig);
  }
}
