import { Component, OnInit} from '@angular/core';
import { faPlus } from '@fortawesome/free-solid-svg-icons';
import {MatDialog, MatDialogConfig} from "@angular/material/dialog";
import {AddPipeDialogComponent} from "../../dialogs/pipe/add-pipe-dialog/add-pipe-dialog.component";
import Pipe from "../../models/pipe";
import {Router} from "@angular/router";
import RequestData, { RequestMethod } from 'src/app/models/request-data';
import { ServerRequestDialogComponent } from 'src/app/dialogs/server-request-dialog/server-request-dialog.component';
import Data, { DataType } from 'src/app/models/data';

@Component({
  selector: 'app-pipes-list-page',
  templateUrl: './pipes-list-page.component.html',
  styleUrls: ['./pipes-list-page.component.scss', '../../styles/base/responsive_table.scss']
})
export class PipesListPageComponent implements OnInit {
  faPlus = faPlus;

  pipes: Pipe[] = []

  constructor(private dialog: MatDialog, private router: Router) {
    let pipe = new Pipe(1, "بررسی تولیدکنندگان");
    pipe.sourceData = new Data(1, "تولیدکنندگان موبایل", DataType.sqlServer);
    pipe.destinationData = new Data(2, "پایگاه داده‌ی مقصد", DataType.sqlServer);
    this.pipes = [pipe];
  }

  addPipeTapped() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.autoFocus = true;
    this.dialog.open(AddPipeDialogComponent, dialogConfig);
  }

  async pipeTapped(pipe: Pipe) {
    await this.router.navigateByUrl("pipeline/" + pipe.id);
  }

  ngOnInit(): void {
    return;
    const dialogConfig = new MatDialogConfig();
    dialogConfig.autoFocus = true;
    dialogConfig.disableClose = true;
    dialogConfig.data = new RequestData("mock/pipeline", RequestMethod.GET, {}, (statusCode: number, result: string) => {
      console.log(result);
      // let arr = JSON.parse(result);
      // this.pipes = [];
      // for (const arrElement of arr) {
      //   this.pipes.push(new Data(arrElement.id, arrElement.tableName, DataType.sqlServer));
      // }
    }); // TODO: This should be changed, obviously
    this.dialog.open(ServerRequestDialogComponent, dialogConfig);
  }
}
