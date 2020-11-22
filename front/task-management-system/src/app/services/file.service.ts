import { HttpClient, HttpHeaders } from '@angular/common/http';
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

  exportTaskToCSV(taskId: number) {
    const headers =  new HttpHeaders({
      'Accept': 'application/csv'
    });
    this.httpClient.get(`${this.apiUrl}file/exportTaskToCSV/${taskId}`, { headers: headers, responseType: 'blob'})
    .subscribe((data) => {
      const blob = new Blob([data], { type: 'application/csv' });
      const objecURL = window.URL.createObjectURL(blob);
      const downloadLink = document.createElement('a');
      downloadLink.href = objecURL;
      downloadLink.download = `Task${taskId}.csv`;
      downloadLink.click();
      window.URL.revokeObjectURL(objecURL);
      downloadLink.remove();
    });
  }
}
