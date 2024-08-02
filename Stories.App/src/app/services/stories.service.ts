import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Story } from './../models/story.type'
import { catchError } from 'rxjs/operators';
import { throwError  } from 'rxjs';
import { environment } from './../environment/environment';

@Injectable({
  providedIn: 'root'
})
export class StoriesService {
  apiHost = 'http://localhost:5079'
  constructor(
    private httpClient: HttpClient
  ) { }
  getStories(orderBy: string, limit: number): Observable<Story[]>
  {

    return this.httpClient.get<Story[]>(`${this.apiHost}/stories-api/Story/GetStories?OrderBy=${orderBy}&Limit=${environment.pageSize}`)
    .pipe(
      catchError(err => {
        return throwError(() => new Error( err.status + ' ' + err.error.messages[0].text));
    }));
  }

}
