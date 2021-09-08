import { Component, OnInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import {LoginStatusService} from "../../services/login-status/login-status.service";

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {
  constructor(public loginStatusService: LoginStatusService
    ,private router:Router) { }
  showNav !:boolean;

  ngOnInit(): void {
    this.router.events.subscribe((val) => {
      if (val instanceof NavigationEnd) {
          if(this.router.url==='/not-found'){
            this.showNav=false;
          }else{
            this.showNav=true;
          }
      }
    });
  }

}
