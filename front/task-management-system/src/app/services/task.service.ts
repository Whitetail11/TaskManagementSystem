import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { API_URL } from '../app-injection-token';
import { Task } from '../models/task';

@Injectable({
  providedIn: 'root'
})
export class TaskService {

  constructor(
    private httpClient: HttpClient,
    @Inject(API_URL) private apiUrl: string
  ) { }
  post(task: Task, email: string): Observable<Task> {
    return this.httpClient.post<Task>(`${this.apiUrl}task?email=${email}`, task);
  }
}
