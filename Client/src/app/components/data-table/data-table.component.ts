import { literalMap } from '@angular/compiler';
import { Component, EventEmitter, HostListener, Input, OnInit, Output } from '@angular/core';
import ActionItem, { ActionType, AggregateActionConfig, FilterActionConfig } from 'src/app/models/action';

@Component({
  selector: 'app-data-table',
  templateUrl: './data-table.component.html',
  styleUrls: ['./data-table.component.scss']
})
export class DataTableComponent implements OnInit {

  @Input() originalActions: ActionItem[] = [];

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

  toBeJoinedDataSource = [
    { vendor: "اپل", year: "2021", revenue: "347000000000" },
    { vendor: "سامسونگ", year: "2021", revenue: "224000000000" },
    { vendor: "شیائومی", year: "2021", revenue: "44000000000" },
    { vendor: "اوپو", year: "2021", revenue: "786000000" },

    { vendor: "اپل", year: "2020", revenue: "294000000000" },
    { vendor: "سامسونگ", year: "2020", revenue: "236000000000" },
    { vendor: "شیائومی", year: "2020", revenue: "38000000000" },
    { vendor: "اوپو", year: "2020", revenue: "524000000" },

    { vendor: "اپل", year: "2019", revenue: "267000000000" },
    { vendor: "سامسونگ", year: "2019", revenue: "230000000000" },
    { vendor: "شیائومی", year: "2019", revenue: "31000000000" },
    { vendor: "اوپو", year: "2019", revenue: "292000000" },
  ];
  // [
  //   {position: 1, name: "سلام"},
  //   {position: 2, name: "خدا حافظ"},
  //   {position: 2, name: "خدا حافظ"},
  //   {position: 2, name: "خدا حافظ"},
  //   {position: 2, name: "خدا حافظ"},
  //   {position: 2, name: "خدا حافظ"},
  //   {position: 2, name: "خدا حافظ"},
  //   {position: 2, name: "خدا حافظ"},
  //   {position: 2, name: "خدا حافظ"},
  //   {position: 2, name: "خدا حافظ"},
  //   {position: 2, name: "خدا حافظ"},
  //   {position: 2, name: "خدا حافظ"},
  //   {position: 2, name: "خدا حافظ"},
  //   {position: 2, name: "خدا حافظ"},
  //   {position: 2, name: "خدا حافظ"},
  //   {position: 2, name: "خدا حافظ"},
  // ]

  dataSource = this.originalDataSource;

  @HostListener('document:keydown.escape', ['$event']) onKeydownHandler(event: KeyboardEvent) {
    this.reloadTapped();
  }

  reloadTapped() {
    this.populateDataSource();
  }

  populateDataSource() {
    this.displayedColumns = ["vendor", "year", "number"];
    let dataSource = [];
    for (const d of this.originalDataSource) {
      dataSource.push(d);
    }

    for (const action of this.originalActions) {
      switch (action.type) {
        case ActionType.aggregate: 
          let aConfig = action.config as AggregateActionConfig;
          let newDataSource: any[] = [];
          


          break;


        case ActionType.join: 
          // TODO: Important: JOIN!
          for (let i = 0; i < dataSource.length; i++) {
            if (i >= this.toBeJoinedDataSource.length) continue;
            dataSource[i].revenue = this.toBeJoinedDataSource[i].revenue;
          }

          this.displayedColumns.push("revenue");
          break;
        case ActionType.filter:
          let f = action.config as FilterActionConfig;
          switch (f.columnName) {
            case "سازنده":
              if (f.columnEqual != "") dataSource = dataSource.filter((d) => d.vendor == f.columnEqual);
              break;
            case "سال":
              if (f.columnEqual != "") dataSource = dataSource.filter((d) => d.year == f.columnEqual);
              if (f.columnLess != "") dataSource = dataSource.filter((d) => d.year < f.columnLess);
              if (f.columnMore != "") dataSource = dataSource.filter((d) => d.year > f.columnMore);
              break;
            case "تعداد فروش":
              if (f.columnEqual != "") dataSource = dataSource.filter((d) => d.number == f.columnEqual);
              if (f.columnLess != "") dataSource = dataSource.filter((d) => d.number < f.columnLess);
              if (f.columnMore != "") dataSource = dataSource.filter((d) => d.number > f.columnMore);
              break;
          }
          break;
          
      }
    }

    this.dataSource = dataSource;
  }

  displayedColumns = ["vendor", "year", "number"];

  constructor() { }

  ngOnInit(): void {
  }

}
