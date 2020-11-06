import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { API_URL } from '../app-injection-token';
import { CreateComment } from '../models/createComment';

@Injectable({
  providedIn: 'root'
})
export class CommentService {

  constructor(private httpClient: HttpClient, 
    @Inject(API_URL) private apiUrl: string) { }

  post(createComment: CreateComment): Observable<Object> {
    return this.httpClient.post(`${this.apiUrl}comment`, createComment);
  }
}
