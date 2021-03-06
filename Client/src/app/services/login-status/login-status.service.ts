import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LoginStatusService {
  isLoggedIn(): boolean {
    return localStorage.getItem("is-logged-in") == "true";
  }

  logout() {
    localStorage.setItem("is-logged-in", "false");
  }
}
