import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http'
import { JwtHelperService } from '@auth0/angular-jwt'
import { Router } from '@angular/router'
import { Token } from '../models/token'
import { LoginModel } from '../models/loginModel';
import { API_URL } from '../app-injection-token';
import { AppConstants } from '../models/appConstants';

export const ACCESS_TOKEN_KEY = 'access_token'

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(
    private http: HttpClient,
    @Inject(API_URL) private apiUrl: string,
    private jwtHelper: JwtHelperService,
    private router: Router
  ) { }

  login(loginModel: LoginModel): Observable<Token> {
    return this.http.post<Token>(`${this.apiUrl}account/login`, loginModel)
    .pipe(
      tap(token => {
        localStorage.setItem(ACCESS_TOKEN_KEY, token.access_token);
        console.log(this.getUserId());
      })
    );
  }

  logout(): void {
    localStorage.removeItem(ACCESS_TOKEN_KEY);
    this.router.navigate(['']);
  }

  register(registerModel): Observable<Token> {
    return this.http.post<Token>(`${this.apiUrl}account/register`, registerModel).pipe(
      tap(token => {
        localStorage.setItem(ACCESS_TOKEN_KEY, token.access_token);
      })
    );
  }

  createUser(createUserModel): Observable<Object> {
    return this.http.post(`${this.apiUrl}account/CreateUser`, createUserModel);
  }

  isAuthenticated(): boolean {
    const token = localStorage.getItem(ACCESS_TOKEN_KEY);
    return token && !this.jwtHelper.isTokenExpired(token);
  }

  getUserId(): string {
    if (!this.isAuthenticated()) {
      this.router.navigate(['login']);
      return "";
    }
    return this.decodeToken().sub;
  }

  getUserRole(): string {
    if (!this.isAuthenticated()) {
      return "";
    }
    return this.decodeToken().role;
  }

  isAdministrator(): boolean {
    return this.getUserRole() == AppConstants.ADMIN_ROLE_NAME;
  }

  isCustomer(): boolean {
    return this.getUserRole() == AppConstants.CUSTOMER_ROLE_NAME;
  }

  isExecutor(): boolean {
    return this.getUserRole() == AppConstants.EXECUTOR_ROLE_NAME;
  }

  decodeToken() {
    const token = localStorage.getItem(ACCESS_TOKEN_KEY);
    return this.jwtHelper.decodeToken(token);
  }
}
