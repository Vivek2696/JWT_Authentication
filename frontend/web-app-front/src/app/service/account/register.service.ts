import { Injectable } from '@angular/core';

import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { User } from '../../models/User';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {

  //TO DO: Change URL
  private loginUrl = 'http://localhost:####';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(
    private http: HttpClient
  ) {
  }

  registerUser(data: any): Observable<User>{
    return this.http.post<User>(this.loginUrl, data, this.httpOptions).pipe(
      tap((newUser: User) => console.log('user registered!')),
      catchError(this.handleError<User>('login'))
    );
  }

  //This function is to handle the error
  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      console.error(error); // log to console instead

      console.log(`${operation} failed: ${error.message}`);

      return of(result as T);
    }
  }
}
