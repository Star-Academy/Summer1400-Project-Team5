import { Component, OnInit } from '@angular/core';
import {LoginStatusServiceService} from "../../login-status-service.service";

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent {
  constructor(public loginStatusServiceService: LoginStatusServiceService) { }
}
