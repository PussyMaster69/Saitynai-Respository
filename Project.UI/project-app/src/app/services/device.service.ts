import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';
import { catchError, map, tap } from 'rxjs/operators';


import { Pair } from '../interfaces/pair';
import { PairExtended } from '../interfaces/pair-extended';
import { Settings } from '../settings';


const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};


@Injectable()
export class DeviceService {

  constructor(private httpClient: HttpClient) { }

  public getPair(id: number): Observable<PairExtended> {
    const url = `${Settings.API_ORIGIN_API}/api/pair/${id}`;
    return this.httpClient.get<PairExtended>(url).pipe(
      tap(_ => this.log(`fetched device id=${id}`)),
      catchError(this.handleError<PairExtended>(`getDevice id=${id}`))
    );
  }

  // public updatePair(pair: Pair): Observable<Pair> {
  //   const url = `${Settings.API_ORIGIN_API}/api/pair/${pair.Id}`;
  //   return this.httpClient.put<Pair>(url, pair, httpOptions).pipe(
  //     tap(_ => this.log(`updated pair id=${pair.Id}`)),
  //     catchError(this.handleError<any>('updatePair'))
  //   );
  // }

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
