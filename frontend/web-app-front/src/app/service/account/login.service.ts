import { Injectable } from '@angular/core';

import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';

import { User } from '../../models/User' //For example we need user data type here so I am importing

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  //TO DO: URL
  private loginUrl = 'http://localhost:49411/api/auth/login'; //update URL

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(
    private http: HttpClient,
  ) {
  }

  authenticateUser(data: any): Observable<any>{
    return this.http.post<any>(this.loginUrl, data, this.httpOptions).pipe(
      tap((newUser)=> {console.log('User Authenticated')}),
      catchError(this.handleError<User>('login'))
    )
  }

  //This function is to handle the error
  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      console.error(error);

      console.log(`${operation} failed: ${error.message}`);

      return of(result as T);
    };
  }
}
