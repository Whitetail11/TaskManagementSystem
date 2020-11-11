import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http'
import { JwtHelperService } from '@auth0/angular-jwt'
import { Router } from '@angular/router'
import { Token } from '../models/token'
import { Login } from '../models/login';
import { CreateUser } from '../models/createUser';
import { Register } from '../models/register';
import { API_URL } from '../app-injection-token';
import { AppConstants } from '../models/appConstants';
import { SelectUser } from '../models/selectUser';
import { ShowUser } from '../models/showUser';

export const ACCESS_TOKEN_KEY = 'access_token'

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(
    private httpClient: HttpClient,
    @Inject(API_URL) private apiUrl: string,
    private jwtHelper: JwtHelperService,
    private router: Router
  ) { }

  get(id: string): Observable<ShowUser> {
    return this.httpClient.get<ShowUser>(`${this.apiUrl}account/${id}`)
  }

  login(login: Login): Observable<Token> {
    return this.httpClient.post<Token>(`${this.apiUrl}account/login`, login)
    .pipe(
      tap(token => {
        localStorage.setItem(ACCESS_TOKEN_KEY, token.access_token);
        // console.log(this.getUserId());
      })
    );
  }

  logout(): void {
    localStorage.removeItem(ACCESS_TOKEN_KEY);
    this.router.navigate(['']);
  }

  register(register: Register): Observable<Token> {
    return this.httpClient.post<Token>(`${this.apiUrl}account/register`, register).pipe(
      tap(token => {
        localStorage.setItem(ACCESS_TOKEN_KEY, token.access_token);
      })
    );
  }

  createUser(createUser: CreateUser): Observable<{}> {
    return this.httpClient.post(`${this.apiUrl}account/CreateUser`, createUser);
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
    return this.decodeToken().userid;
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

  getExecutorsForSelect(): Observable<SelectUser[]> {
    return this.httpClient.get<SelectUser[]>(`${this.apiUrl}account/getExecutorsForSelect`);
  }

  sendEmailConfirmationLink(): Observable<{}> {
    return this.httpClient.get(`${this.apiUrl}account/SendEmailConfirmationLink`);
  }

  confirmEmail(userId: string, code: string): Observable<string> {
    return this.httpClient.get<string>(`${this.apiUrl}account/confirmEmail?userId=${userId}&code=${code}`);
  }
}
