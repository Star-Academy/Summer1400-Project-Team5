import {Component, Input, OnInit} from '@angular/core';
import Data, {CSVDataConfig, DataType, SQLServerDataConfig} from "../../../models/data";

@Component({
  selector: 'app-csv-server-data-details-dialog',
  templateUrl: './csv-server-data-details-dialog.component.html',
  styleUrls: ['./csv-server-data-details-dialog.component.scss']
})
export class CsvServerDataDetailsDialogComponent implements OnInit {
  @Input() data = new Data(0, "", DataType.csv);
  fileAddress = "";

  constructor() { }

  ngOnInit(): void {
    const config = this.data.config as CSVDataConfig;
    this.fileAddress = config.fileAddress;
  }

}
