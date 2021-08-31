import { Component, OnInit } from '@angular/core';
import { faCogs } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-edit-action-dialog',
  templateUrl: './edit-action-dialog.component.html',
  styleUrls: ['./edit-action-dialog.component.scss']
})
export class EditActionDialogComponent implements OnInit {
  faCogs = faCogs;

  constructor() { }

  ngOnInit(): void {
  }

}
