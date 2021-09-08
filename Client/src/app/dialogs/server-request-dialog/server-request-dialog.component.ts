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
    let rawResponse, result, statusCode;
    switch (this.requestData.method) {
      case RequestMethod.GET:
        // result = await this.http.get<any>(RequestData.server + "/" + this.requestData.endpoint, {
        //   headers: {}, // TODO: Header token ... also handling null token state
        // }).toPromise();

        rawResponse = await fetch(RequestData.server + "/" + this.requestData.endpoint, {
          method: 'GET',
          headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
          },
          body: JSON.stringify(this.requestData.body)
        });
        statusCode = rawResponse.status;
        result = await rawResponse.json();

        break;
      case RequestMethod.POST:
        // result = await this.http.post<any>(RequestData.server + "/" + this.requestData.endpoint, this.requestData.body, {
        //   headers: {}, // TODO: Header token ... also handling null token state
        // }).toPromise();

        rawResponse = await fetch(RequestData.server + "/" + this.requestData.endpoint, {
          method: 'POST',
          headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
          },
          body: JSON.stringify(this.requestData.body)
        });
        statusCode = rawResponse.status;
        result = await rawResponse.json();

        break;
    }
    this.requestData.callback(statusCode, result);
    this.dialogRef.close();
  }

}
