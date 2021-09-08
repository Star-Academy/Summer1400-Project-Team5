import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.scss'],
})
export class LoginPageComponent implements OnInit {
  form: any = {
    email: '',
    password: '',
  };

  constructor() {}

  ngOnInit(): void {}

  submit() {}

  success(token: string) {
    localStorage.setItem('is-logged-in', 'true');
  }
}
