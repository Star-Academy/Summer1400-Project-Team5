import { Component, OnInit } from '@angular/core';
import { faPlusSquare } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-data-list-page',
  templateUrl: './data-list-page.component.html',
  styleUrls: ['./data-list-page.component.scss', '../../styles/base/responsive_table.scss']
})
export class DataListPageComponent {
  faPlusSquare = faPlusSquare;
}
