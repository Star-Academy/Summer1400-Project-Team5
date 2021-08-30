import {Component, Inject, Input, OnInit} from '@angular/core';
import Data, {CSVDataConfig, DataType, SQLServerDataConfig} from "../../../models/data";
import {MAT_DIALOG_DATA} from '@angular/material/dialog';


@Component({
  selector: 'app-csv-server-data-details-dialog',
  templateUrl: './c-s-v-data-details-dialog.component.html',
  styleUrls: ['./c-s-v-data-details-dialog.component.scss']
})
export class CSVDataDetailsDialogComponent implements OnInit {
  fileAddress = "";

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: Data
  ) { }

  ngOnInit(): void {
    const config = this.data.config as CSVDataConfig;
    this.fileAddress = config.fileAddress;
  }

}
