import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LoginStatusServiceService {
  isLoggedIn(): boolean {
    return localStorage.getItem("is-logged-in") == "true";
  }
}
