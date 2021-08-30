import {Component} from '@angular/core';
import {faPlus} from '@fortawesome/free-solid-svg-icons';
import {MatDialog, MatDialogConfig} from "@angular/material/dialog";
import {AddDataDialogComponent} from "../../dialogs/data/add-data-dialog/add-data-dialog.component";
import Data from "../../models/data";
import {DataType} from '../../models/data';
import {CSVDataDetailsDialogComponent} from "../../dialogs/data/csv-server-data-details-dialog/c-s-v-data-details-dialog.component";
import {SQLServerDataDetailsDialogComponent} from "../../dialogs/data/sql-data-details-dialog/s-q-l-server-data-details-dialog.component";

@Component({
  selector: 'app-data-list-page',
  templateUrl: './data-list-page.component.html',
  styleUrls: ['./data-list-page.component.scss', '../../styles/base/responsive_table.scss']
})
export class DataListPageComponent {
  faPlus = faPlus;

  allData: Data[] = [
    new Data(1, "داده‌ی نخست", DataType.csv),
    new Data(2, "داده‌ی دوم", DataType.sqlServer),
    new Data(3, "داده‌ای جدید", DataType.sqlServer),
  ];

  constructor(private dialog: MatDialog) {
  }

  addDataTapped() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.autoFocus = true;
    this.dialog.open(AddDataDialogComponent, dialogConfig);
  }

  dataTapped(data: Data) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.autoFocus = true;
    dialogConfig.data = data;
    switch (data.type) {
      case DataType.csv:
        this.dialog.open(CSVDataDetailsDialogComponent, dialogConfig);
        break;
      case DataType.sqlServer:
        this.dialog.open(SQLServerDataDetailsDialogComponent, dialogConfig);
        break;
    }
  }
}
