import {Component, OnInit} from '@angular/core';

import {faChartPie, faTable, faPlay, faPlus, faCross, faBackspace, faFileExport, faFileImport} from '@fortawesome/free-solid-svg-icons';
import {ActivatedRoute} from "@angular/router";
import Pipe from "../../models/pipe";
import {MatDialog, MatDialogConfig} from "@angular/material/dialog";
import {ServerRequestDialogComponent} from "../../dialogs/server-request-dialog/server-request-dialog.component";
import RequestData, {RequestMethod} from "../../models/request-data";
import ActionItem, { ActionConfig, ActionType, AggregateActionConfig, AggregateType, CalculateActionConfig, CalculateType, FilterActionConfig, JoinActionConfig } from 'src/app/models/action';
import { ComponentFixture } from '@angular/core/testing';

@Component({
  selector: 'app-pipeline',
  templateUrl: './pipeline.component.html',
  styleUrls: ['./pipeline.component.scss']
})
export class PipelineComponent implements OnInit {
  faDatabase = faChartPie;
  faTable = faTable;
  faPlay = faPlay;
  faPlus = faPlus;
  faMinus = faBackspace;
  faExport = faFileExport;
  faImport = faFileImport;
  pipe = new Pipe(1, "تبدیل اول");

  constructor(private route: ActivatedRoute, private dialog: MatDialog) { }

  async loadPipe(id: number): Promise<Pipe> {
    let pipe = new Pipe(2, "بررسی تولیدکنندگان");
    return Promise.resolve(pipe); // TODO: Incomplete implementation
  }

  ngOnInit(): void {
    this.route.queryParams.subscribe(async params => {
      this.pipe = await this.loadPipe(parseInt(params['id']));
    });
  }

  runPipeTapped(): void {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.autoFocus = true;
    dialogConfig.disableClose = true;
    dialogConfig.data = new RequestData("run-pipe", RequestMethod.GET, {}, () => {
      console.log("callback!")
    }); // TODO: This should be changed, obviously
    this.dialog.open(ServerRequestDialogComponent, dialogConfig);
  }

  addActionTapped(): void {
    this.pipe.actions.push(new ActionItem(ActionType.filter, new FilterActionConfig()));
  }

  removeLastActionTapped(): void {
    this.pipe.actions.pop();
  }

  exportTapped(): void {
    let tab = window.open('about:blank', '_blank');
    tab?.document.write(JSON.stringify(this.pipe.actions));
    tab?.document.close();
  }
  
  importTapped(): void {
    navigator.clipboard.readText().then((text: string) => {
      this.pipe.actions = [];
      let object = JSON.parse(text);
      for (const item of object) {
        switch (item.type) {
          case 0: 
            let jConfig = new JoinActionConfig();
            let jAction = new ActionItem(ActionType.join, jConfig);
            this.pipe.actions.push(jAction);
            break;
          case 1: 
            let fConfig = new FilterActionConfig();
            fConfig.columnEqual = item.config.columnEqual;
            fConfig.columnLess = item.config.columnLess;
            fConfig.columnMore = item.config.columnMore;
            fConfig.columnName = item.config.columnName;
            let fAction = new ActionItem(ActionType.filter, fConfig);
            this.pipe.actions.push(fAction);
            break;
          case 2: 
            let aConfig = new AggregateActionConfig();
            aConfig.addColumn = item.config.addColumn;
            aConfig.groupColumn = item.config.groupColumn;
            switch (item.config.type) {
              case 0:
                aConfig.type = AggregateType.sum;
                break;
              case 1:
                aConfig.type = AggregateType.count;
                break;
              case 2:
                aConfig.type = AggregateType.average;
                break;
              case 3:
                aConfig.type = AggregateType.min;
                break;
              case 4:
                aConfig.type = AggregateType.max;
                break;
            }
            let aAction = new ActionItem(ActionType.aggregate, aConfig);
            this.pipe.actions.push(aAction);
            break;
          case 3: 
            let cConfig = new CalculateActionConfig();
            cConfig.firstOperand = item.config.firstOperand;
            cConfig.secondOperand = item.config.secondOperand;
            switch (item.config.type) {
              case 0:
                cConfig.type = CalculateType.add;
                break;
              case 1:
                cConfig.type = CalculateType.subtract;
                break;
              case 2:
                cConfig.type = CalculateType.multiply;
                break;
              case 3:
                cConfig.type = CalculateType.divide;
                break;
            }
            let cAction = new ActionItem(ActionType.calculate, cConfig);
            this.pipe.actions.push(cAction);
            break;
        }
      }
    })
  }
}
