import { Component, OnInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss']
})
export class FooterComponent implements OnInit {

  constructor(private router:Router) { }
  showFooter !:boolean;
  ngOnInit(): void {
    this.router.events.subscribe((val) => {
      if (val instanceof NavigationEnd) {
          if(this.router.url==='/not-found'){
            this.showFooter=false;
          }else{
            this.showFooter=true;
          }
      }
    });
  }


}
