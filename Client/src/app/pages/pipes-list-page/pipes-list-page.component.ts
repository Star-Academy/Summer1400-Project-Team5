import { Component} from '@angular/core';
import { faPlusSquare } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-pipes-list-page',
  templateUrl: './pipes-list-page.component.html',
  styleUrls: ['./pipes-list-page.component.scss', '../../styles/base/responsive_table.scss']
})
export class PipesListPageComponent {
  faPlusSquare = faPlusSquare;
}
