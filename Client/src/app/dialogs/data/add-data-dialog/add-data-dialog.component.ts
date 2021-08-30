import { Component, OnInit } from '@angular/core';
import { faTable} from '@fortawesome/free-solid-svg-icons';
@Component({
  selector: 'app-add-data-dialog',
  templateUrl: './add-data-dialog.component.html',
  styleUrls: ['./add-data-dialog.component.scss']
})
export class AddDataDialogComponent implements OnInit {

  constructor() { }
  faTable=faTable;
  ngOnInit(): void {
  }

}
