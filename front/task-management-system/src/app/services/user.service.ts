import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { API_URL } from '../app-injection-token';
import { Executor } from '../models/Executor';
import { Task } from '../models/task';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(
    private httpClient: HttpClient,
    @Inject(API_URL) private apiUrl: string
  ) {
   }
   get(): Observable<Executor[]> {
     return this.httpClient.get<Executor[]>(`${this.apiUrl}account`);
   }
}
