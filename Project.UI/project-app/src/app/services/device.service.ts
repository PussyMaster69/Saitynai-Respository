import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';
import { catchError, map, tap } from 'rxjs/operators';


import { Pair } from '../interfaces/pair';
import { PairExtended } from '../interfaces/pair-extended';
import { Settings } from '../settings';
import { SessionService } from './session.service'


// const httpOptions = {
//   headers: new HttpHeaders({'Content-Type': 'application/json'})
// };


@Injectable()
export class DeviceService {

  private httpOptions = {
    headers: new HttpHeaders({
      'Authorization': `Bearer ${this.sessionService.getLocalToken()}`,
      'Content-Type': 'application/json'
    })
  };

  constructor(private httpClient: HttpClient, private sessionService: SessionService) {
  }

  public addPair(pair: PairExtended): Observable<Pair> {
    const url = `${Settings.API_ORIGIN_API}/api/pair`;
    return this.httpClient.post<Pair>(url, pair, this.httpOptions);
  }

  public getPair(id: number): Observable<PairExtended> {
    const url = `${Settings.API_ORIGIN_API}/api/pair/${id}`;
    return this.httpClient.get<PairExtended>(url, this.httpOptions).pipe(
      tap(_ => this.log(`fetched device id=${id}`)),
      catchError(this.handleError<PairExtended>(`getDevice id=${id}`))
    );
  }

  public getPairs(): Observable<Pair[]> {
    const url = `${Settings.API_ORIGIN_API}/api/pair`;
    return this.httpClient.get<Pair[]>(url, this.httpOptions);
  }

  public updatePair(pair: Pair): Observable<Pair> {
    const url = `${Settings.API_ORIGIN_API}/api/pair/${pair.id}`;
    return this.httpClient.put<Pair>(url, pair, this.httpOptions).pipe(
      tap(_ => this.log(`updated pair id=${pair.id}`)),
      catchError(this.handleError<any>('updatePair'))
    );
  }

  public deletePair(id: number | string): Observable<Pair> {
    const url = `${Settings.API_ORIGIN_API}/api/pair/${id}`;
    return this.httpClient.delete<Pair>(url, this.httpOptions);
  }

  /** Log a HeroService message with the console */
  private log(message: string) {
    console.log(message);
  }

  private handleError<T> (operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
   
      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead
   
      // TODO: better job of transforming error for user consumption
      this.log(`${operation} failed: ${error.message}`);
   
      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }
}

export interface IPairList {
  pairList: Pair[]
}