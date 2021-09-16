import {Component, OnInit} from '@angular/core';

import {faChartPie, faTable} from '@fortawesome/free-solid-svg-icons';
import {ActivatedRoute} from "@angular/router";
import Pipe from "../../models/pipe";
import {MatDialog, MatDialogConfig} from "@angular/material/dialog";
import {ServerRequestDialogComponent} from "../../dialogs/server-request-dialog/server-request-dialog.component";
import RequestData, {RequestMethod} from "../../models/request-data";
import ActionItem, { ActionConfig, ActionType, FilterActionConfig, JoinActionConfig } from 'src/app/models/action';

@Component({
  selector: 'app-pipeline',
  templateUrl: './pipeline.component.html',
  styleUrls: ['./pipeline.component.scss']
})
export class PipelineComponent implements OnInit {
  faDatabase = faChartPie;
  faTable = faTable;
  pipe = new Pipe(1, "لوله‌ی اول");

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
}
