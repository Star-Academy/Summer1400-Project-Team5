import { Component, OnInit } from '@angular/core';

import { faDatabase, faTable} from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-pipeline',
  templateUrl: './pipeline.component.html',
  styleUrls: ['./pipeline.component.scss']
})
export class PipelineComponent implements OnInit {

  constructor() { }
  faDatabase=faDatabase;
  faTable=faTable;
  ngOnInit(): void {
  }

}
