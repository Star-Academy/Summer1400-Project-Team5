import {Component, Inject, Input, OnInit} from '@angular/core';
import Data, {DataType, SQLServerDataConfig} from "../../../models/data";
import {dateInputsHaveChanged} from "@angular/material/datepicker/datepicker-input-base";
import {MAT_DIALOG_DATA} from "@angular/material/dialog";

@Component({
  selector: 'app-data-details-dialog',
  templateUrl: './s-q-l-server-data-details-dialog.component.html',
  styleUrls: ['./s-q-l-server-data-details-dialog.component.scss']
})
export class SQLServerDataDetailsDialogComponent implements OnInit {
  server = "";
  username = "";
  password = "";

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: Data
  ) { }

  ngOnInit(): void {
    console.log(this.data);
    console.log("j");
    // const config = this.data.config as SQLServerDataConfig;
    // this.server = config.server;
    // this.username = config.username;
    // this.password = config.password;
  }

}
