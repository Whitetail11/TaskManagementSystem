import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { API_URL } from '../app-injection-token';
import { Status } from '../models/status';

@Injectable({
  providedIn: 'root'
})
export class StatusService {

  constructor(private httpClient: HttpClient,
    @Inject(API_URL) private apiUrl: string) { }
  
  get(): Observable<Status[]> {
    return this.httpClient.get<Status[]>(`${this.apiUrl}status`);
  }
  changeStatus(taskId: number, statusId: number) : Observable<any> {
    return this.httpClient.put(`${this.apiUrl}Task/ChangeStatus?taskId=${taskId}&statusId=${statusId}`, '');
  }
}
