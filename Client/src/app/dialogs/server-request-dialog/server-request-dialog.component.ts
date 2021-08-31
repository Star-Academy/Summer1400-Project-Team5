import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import Data from "../../models/data";
import RequestData, {RequestMethod} from "../../models/request-data";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-server-request-dialog',
  templateUrl: './server-request-dialog.component.html',
  styleUrls: ['./server-request-dialog.component.scss']
})
export class ServerRequestDialogComponent implements OnInit {
  constructor(
    private dialogRef: MatDialogRef<ServerRequestDialogComponent>,
    private http: HttpClient,
    @Inject(MAT_DIALOG_DATA) public requestData: RequestData
  ) { }

  async ngOnInit(): Promise<void> {
    let result;
    switch (this.requestData.method) {
      case RequestMethod.GET:
        result = await this.http.get<any>(this.requestData.endpoint, {
          headers: {}, // TODO: Header token ... also handling null token state
        }).toPromise();
        break;
      case RequestMethod.POST:
        result = await this.http.post<any>(this.requestData.endpoint, this.requestData.body, {
          headers: {}, // TODO: Header token ... also handling null token state
        }).toPromise();
        break;
    }
    this.requestData.callback(result);
    this.dialogRef.close();
  }

}
