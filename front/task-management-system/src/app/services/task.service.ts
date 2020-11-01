import { HttpClient, HttpParams } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { API_URL } from '../app-injection-token';
import { Status } from '../models/status';
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
  
  getForPage(taskPage, taskFilter): Observable<TaskShortInfo[]> {
    var params = new HttpParams({ fromObject: { ...taskPage, ...taskFilter } });
    return this.httpClient.get<TaskShortInfo[]>(`${this.apiUrl}task/getForPage`, { params: params });
  }
  
  getTaskCount(taskFilter): Observable<number> {
    var params = new HttpParams({ fromObject: taskFilter });
    return this.httpClient.get<number>(`${this.apiUrl}task/getTaskCount`, { params: params });
  }

  getStatuses(): Observable<Status[]> {
    return this.httpClient.get<Status[]>(`${this.apiUrl}task/getStatuses`);
  }
}
