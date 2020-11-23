import { HttpClient, HttpParams } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { API_URL } from '../app-injection-token';
import { Status } from '../models/status';
import { Task } from '../models/task';
import { ShowTaskShortInfo } from '../models/showTaskShortInfo';
import { ShowTask } from '../models/showTask';

@Injectable({
  providedIn: 'root'
})
export class TaskService {

  constructor(
    private httpClient: HttpClient,
    @Inject(API_URL) private apiUrl: string
  ) { }

  post(task: Task): Observable<number> {
    return this.httpClient.post<number>(`${this.apiUrl}task`, task);
  }

  deleteTask(id: number): Observable<{}> {
    return this.httpClient.delete(`${this.apiUrl}Task?id=${id}`);
  }
  
  editTask(task: Task): Observable<number> {
    return this.httpClient.put<number>(`${this.apiUrl}Task`, task);
  }

  getForPage(taskPage, taskFilter): Observable<ShowTaskShortInfo[]> {
    var params = new HttpParams({ fromObject: { ...taskPage, ...taskFilter } });
    return this.httpClient.get<ShowTaskShortInfo[]>(`${this.apiUrl}task/getForPage`, { params: params });
  }

  getForShowig(id: number): Observable<ShowTask> {
    return this.httpClient.get<ShowTask>(`${this.apiUrl}task/getForShowing/${id}`);
  }

  getTaskCount(taskFilter): Observable<number> {
    var params = new HttpParams({ fromObject: taskFilter });
    return this.httpClient.get<number>(`${this.apiUrl}task/getTaskCount`, { params: params });
  }

  getStatuses(): Observable<Status[]> {
    return this.httpClient.get<Status[]>(`${this.apiUrl}task/getStatuses`);
  }

  getTaskById(id: number): Observable<Task> {
    return this.httpClient.get<Task>(`${this.apiUrl}task?id=${id}`)
  }
}
