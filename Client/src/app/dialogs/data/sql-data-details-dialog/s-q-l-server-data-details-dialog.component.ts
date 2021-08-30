import {Component, Input, OnInit} from '@angular/core';
import Data, {DataType, SQLServerDataConfig} from "../../../models/data";
import {dateInputsHaveChanged} from "@angular/material/datepicker/datepicker-input-base";

@Component({
  selector: 'app-data-details-dialog',
  templateUrl: './s-q-l-server-data-details-dialog.component.html',
  styleUrls: ['./s-q-l-server-data-details-dialog.component.scss']
})
export class SQLServerDataDetailsDialogComponent implements OnInit {
  @Input() data = new Data(0, "", DataType.csv);
  server = "";
  username = "";
  password = "";

  constructor() { }

  ngOnInit(): void {
    const config = this.data.config as SQLServerDataConfig;
    this.server = config.server;
    this.username = config.username;
    this.password = config.password;
  }

}
