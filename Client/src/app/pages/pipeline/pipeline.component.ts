import { Component, OnInit } from '@angular/core';

import { faDatabase, faTable} from '@fortawesome/free-solid-svg-icons';
import {ActivatedRoute} from "@angular/router";
import Pipe from "../../models/pipe";

@Component({
  selector: 'app-pipeline',
  templateUrl: './pipeline.component.html',
  styleUrls: ['./pipeline.component.scss']
})
export class PipelineComponent implements OnInit {
  faDatabase = faDatabase;
  faTable = faTable;
  pipe = new Pipe(1, "لوله‌ی اول");

  constructor(private route: ActivatedRoute) { }

  async loadPipe(id: number): Promise<Pipe> {
    return Promise.resolve(new Pipe(2, "لوله‌ی دوم"));
  }

  ngOnInit(): void {
    this.route.queryParams.subscribe(async params => {
      this.pipe = await this.loadPipe(parseInt(params['id']));
    });
  }

}
