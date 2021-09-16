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

  isNumber(value: string | number): boolean {
    return ((value != null) &&
            (value !== '') &&
            !isNaN(Number(value.toString())));
  }

  findItemInDataSource(item: any, newDataSource: any, groupColumn: string, addColumn: string): boolean {
    for (const newItem of newDataSource) {
      if (newItem[groupColumn] == item[groupColumn]) {
        for (const key in item) {
          if (item.hasOwnProperty(key) && key != groupColumn && key != addColumn) {
            if (this.isNumber(item[key]) && this.isNumber(newItem[key])) {
              let newInt = parseInt(newItem[key]) + parseInt(item[key]);
              newItem[key] = newInt + "";
            } else {
              newItem[key] += item[key] + " ";
            }
          }
        }
        return true;
      }
    }
    return false;
  }

  findColumnNameInCode(persian: string) {
    switch (persian) {
      case "سازنده": return "vendor";
      case "سال": return "year";
      case "تعداد فروش": return "number";
      case "میزان درآمد": return "revenue";
      default: return "";
    }
  }

  aggregate(action: ActionItem, config: AggregateActionConfig, dataSource: any): any { // returns newDataSource
    let newDataSource: any[] = [];
    let groupColumn = this.findColumnNameInCode(config.groupColumn);
    let addColumn = this.findColumnNameInCode(config.addColumn);
    const index = this.displayedColumns.indexOf(addColumn);
    if (index > -1) {
      this.displayedColumns.splice(index, 1);
    }
    for (const item of dataSource) {
      if (!this.findItemInDataSource(item, newDataSource, groupColumn, addColumn)) {
        let newItem: any = {};

        for (const key in item) {
          if (item.hasOwnProperty(key) && key != addColumn) {
            newItem[key] = item[key];
          }
        }

        newDataSource.push(newItem);
      }
    }
    return newDataSource;
  }

  populateDataSource() {
    this.displayedColumns = ["vendor", "year", "number"];
    let dataSource = [];
    for (const d of this.originalDataSource) {
      dataSource.push(d);
    }

    for (const action of this.originalActions) {
      switch (action.type) {
        case ActionType.calculate: return // TODO: OOOO
        case ActionType.aggregate: 
          let aConfig = action.config as AggregateActionConfig;
          dataSource = this.aggregate(action, aConfig, dataSource);
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
