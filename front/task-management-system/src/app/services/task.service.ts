import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { API_URL } from '../app-injection-token';
import { TasksComponent } from '../components/tasks/tasks.component';
import { Task } from '../models/task';
import { TaskShortInfo } from '../models/taskShortInfo';

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
  changeStatus(taskId: number, statusId: number) : Observable<{}>
  {
    return this.httpClient.put(`${this.apiUrl}Task/ChangeStatus?taskId=${taskId}&statusId=${statusId}`, []);
  }
  deleteTask(id: number) : Observable<{}>
  {
    return this.httpClient.delete(`${this.apiUrl}Task?id=${id}`);
  }
  getForPage(pageNumber: number, pageSize: number): Observable<TaskShortInfo[]> {
    return this.httpClient.get<TaskShortInfo[]>(`${this.apiUrl}task/getForPage?pageNumber=${pageNumber}&pageSize=${pageSize}`)
  }
  getTaskCount(): Observable<number> {
    return this.httpClient.get<number>(`${this.apiUrl}task/getTaskCount`);
  }
}
