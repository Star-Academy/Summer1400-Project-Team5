import {Component, Inject, Input, OnInit} from '@angular/core';
import Data, {CSVDataConfig, DataType, SQLServerDataConfig} from "../../../models/data";
import {MAT_DIALOG_DATA} from '@angular/material/dialog';


@Component({
  selector: 'app-csv-server-data-details-dialog',
  templateUrl: './c-s-v-data-details-dialog.component.html',
  styleUrls: ['./c-s-v-data-details-dialog.component.scss', '../../../styles/base/dialogs.scss']
})
export class CSVDataDetailsDialogComponent implements OnInit {
  fileAddress = "";

  originalDataSource: any[] = [
    { vendor: "اپل", year: "2021", number: "141000000" },
    { vendor: "سامسونگ", year: "2021", number: "188000000" },
    { vendor: "شیائومی", year: "2021", number: "169000000" },
    { vendor: "اوپو", year: "2021", number: "105000000" },

    { vendor: "اپل", year: "2020", number: "234000000" },
    { vendor: "سامسونگ", year: "2020", number: "191000000" },
    { vendor: "شیائومی", year: "2020", number: "112000000" },
    { vendor: "اوپو", year: "2020", number: "88000000" },

    { vendor: "اپل", year: "2019", number: "130000000" },
    { vendor: "سامسونگ", year: "2019", number: "218000000" },
    { vendor: "شیائومی", year: "2019", number: "91000000" },
    { vendor: "اوپو", year: "2019", number: "87000000" },
  ];

  downloadCSVTapped(): void {
    const items = this.originalDataSource
    const replacer = (key: any, value: any) => value === null ? 'null' : value
    const header = Object.keys(items[0])
    const csv = [
      header.join(','),
      ...items.map((row: any) => header.map(fieldName => JSON.stringify(row[fieldName], replacer)).join(','))
    ].join('<br>')
    let tab = window.open('about:blank', '_blank');
    tab?.document.write(csv);
    tab?.document.close();
  }

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: Data
  ) { }

  ngOnInit(): void {
    const config = this.data.config as CSVDataConfig;
    this.fileAddress = config.fileAddress;
  }

}
