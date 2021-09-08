import {Component, Inject, OnInit} from '@angular/core';
import { faTable} from '@fortawesome/free-solid-svg-icons';
import {MAT_DIALOG_DATA, MatDialog, MatDialogConfig, MatDialogRef} from "@angular/material/dialog";
import Data from "../../../models/data";
import RequestData, {RequestMethod} from "../../../models/request-data";
import {ServerRequestDialogComponent} from "../../server-request-dialog/server-request-dialog.component";
@Component({
  selector: 'app-add-data-dialog',
  templateUrl: './add-data-dialog.component.html',
  styleUrls: ['./add-data-dialog.component.scss', '../../../styles/base/dialogs.scss']
})
export class AddDataDialogComponent implements OnInit {

  constructor(
    private dialogRef: MatDialogRef<AddDataDialogComponent>,
    private dialog: MatDialog,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) { }

  faTable = faTable;

  sourceTable = "";
  destinationTable = "";
  serverName = "";
  databaseName = "";
  username = "";
  password = "";

  ngOnInit(): void {
  }

  addTapped() {
    let json = {
      SourceTable: this.sourceTable,
      DestTable: this.destinationTable,
      ConnectionString: {
        ServerName: this.serverName,
        DatabaseName: this.databaseName,
        Username: this.username,
        Password: this.password
      }
    };

    const dialogConfig = new MatDialogConfig();
    dialogConfig.autoFocus = true;
    dialogConfig.disableClose = true;
    dialogConfig.data = new RequestData("data/connectsql", RequestMethod.POST, json, () => {

    }); // TODO: This should be changed, obviously
    this.dialog.open(ServerRequestDialogComponent, dialogConfig);
  }
}
