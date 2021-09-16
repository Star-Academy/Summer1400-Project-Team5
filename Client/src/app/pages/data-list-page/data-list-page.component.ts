import {Component, OnInit} from '@angular/core';
import {faPlus} from '@fortawesome/free-solid-svg-icons';
import {MatDialog, MatDialogConfig} from "@angular/material/dialog";
import {AddDataDialogComponent} from "../../dialogs/data/add-data-dialog/add-data-dialog.component";
import Data, {DataType} from "../../models/data";
import {CSVDataDetailsDialogComponent} from "../../dialogs/data/csv-server-data-details-dialog/c-s-v-data-details-dialog.component";
import {SQLServerDataDetailsDialogComponent} from "../../dialogs/data/sql-data-details-dialog/s-q-l-server-data-details-dialog.component";
import RequestData, {RequestMethod} from "../../models/request-data";
import {ServerRequestDialogComponent} from "../../dialogs/server-request-dialog/server-request-dialog.component";

@Component({
  selector: 'app-data-list-page',
  templateUrl: './data-list-page.component.html',
  styleUrls: ['./data-list-page.component.scss', '../../styles/base/responsive_table.scss']
})
export class DataListPageComponent implements OnInit {
  faPlus = faPlus;

  allData: Data[] = [
    new Data(1, "تولیدکنندگان موبایل", DataType.csv),
    new Data(2, "پایگاه داده‌ی مقصد", DataType.sqlServer),
  ];

  constructor(private dialog: MatDialog) {
  }

  addDataTapped() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.autoFocus = true;
    dialogConfig.disableClose = false;
    dialogConfig.data = (newData: Data) => this.allData.push(newData);
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

  ngOnInit(): void {
    return;
    const dialogConfig = new MatDialogConfig();
    dialogConfig.autoFocus = true;
    dialogConfig.disableClose = true;
    dialogConfig.data = new RequestData("mock/data/datasource-list", RequestMethod.GET, {}, (statusCode: number, result: string) => {
      let arr = JSON.parse(result);
      this.allData = [];
      for (const arrElement of arr) {
        this.allData.push(new Data(arrElement.id, arrElement.tableName, DataType.sqlServer));
      }
    }); // TODO: This should be changed, obviously
    this.dialog.open(ServerRequestDialogComponent, dialogConfig);
  }
}
