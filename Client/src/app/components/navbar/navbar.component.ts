import { Component, OnInit } from '@angular/core';
import {LoginStatusService} from "../../services/login-status/login-status.service";

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent {
  constructor(public loginStatusService: LoginStatusService) { }
}
