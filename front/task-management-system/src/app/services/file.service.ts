import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { API_URL } from '../app-injection-token';

@Injectable({
  providedIn: 'root'
})
export class FileService {

  constructor(private httpClient: HttpClient,
    @Inject(API_URL) private apiUrl: string) { }
  
  postFile(fd: FormData, taskId: number): Observable<{}> {
    return this.httpClient.post(`${this.apiUrl}file?TaskId=${taskId}`, fd)
  }
  
  downloadFiles(taskId: number) : Observable<any> {
    return this.httpClient.get<any>(`${this.apiUrl}file/getFilesByTaskId?taskId=${taskId}`, {observe: 'response', responseType: 'blob' as 'json'});
  }

  deletefile(id: number) : Observable<{}> {
    return this.httpClient.delete(`${this.apiUrl}file?id=${id}`);
  }
}
