import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { API_URL } from '../app-injection-token';
import { CreateComment } from '../models/createComment';
import { ShowComment } from '../models/showComment';

@Injectable({
  providedIn: 'root'
})
export class CommentService {

  constructor(private httpClient: HttpClient, 
    @Inject(API_URL) private apiUrl: string) { }
  
  getForTask(taskId: number): Observable<ShowComment[]> {
    return this.httpClient.get<ShowComment[]>(`${this.apiUrl}comment/getForTask/${taskId}`)
  }

  post(createComment: CreateComment): Observable<Object> {
    return this.httpClient.post(`${this.apiUrl}comment`, createComment);
  }

  delete(id: number, taskId: number): Observable<Object> {
    return this.httpClient.delete(`${this.apiUrl}comment/${id}?taskId=${taskId}`);
  }
}
