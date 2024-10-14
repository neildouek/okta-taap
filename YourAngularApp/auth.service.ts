import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private tokenKey = 'token';

  constructor(private http: HttpClient, private router: Router) {}

  public isAuthenticated(): boolean {
    return !!localStorage.getItem(this.tokenKey);
  }

  public login() {
    window.location.href = 'https://your-okta-saml-login-url';
  }

  public handleSAMLResponse(samlResponse: string) {
    // Store the token from the backend or Okta response
    localStorage.setItem(this.tokenKey, samlResponse);
    this.router.navigate(['/']);
  }

  public logout() {
    localStorage.removeItem(this.tokenKey);
    this.router.navigate(['/login']);
  }
}