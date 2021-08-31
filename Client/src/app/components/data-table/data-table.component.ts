import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-data-table',
  templateUrl: './data-table.component.html',
  styleUrls: ['./data-table.component.scss']
})
export class DataTableComponent implements OnInit {

  dataSource = [
    {position: 1, name: "سلام"},
    {position: 2, name: "خدا حافظ"},
    {position: 2, name: "خدا حافظ"},
    {position: 2, name: "خدا حافظ"},
    {position: 2, name: "خدا حافظ"},
    {position: 2, name: "خدا حافظ"},
    {position: 2, name: "خدا حافظ"},
    {position: 2, name: "خدا حافظ"},
    {position: 2, name: "خدا حافظ"},
    {position: 2, name: "خدا حافظ"},
    {position: 2, name: "خدا حافظ"},
    {position: 2, name: "خدا حافظ"},
    {position: 2, name: "خدا حافظ"},
    {position: 2, name: "خدا حافظ"},
    {position: 2, name: "خدا حافظ"},
    {position: 2, name: "خدا حافظ"},
  ]

  displayedColumns = ["position", "name"];

  constructor() { }

  ngOnInit(): void {
  }

}
