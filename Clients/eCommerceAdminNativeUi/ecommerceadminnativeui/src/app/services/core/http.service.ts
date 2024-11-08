import { Injectable } from '@angular/core';
import {
  HttpClient,
  HttpErrorResponse,
  HttpHeaders,
} from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class HttpService {
  constructor(private http: HttpClient) {}

  public post<T>(
    url: string,
    data: any,
    takeUntil$: Observable<void>,
    onNext: (data: T) => void,
    onError: (error: HttpErrorResponse) => void = async (err) => {
      console.log(err);
    },
    onComplete: () => void = () => {
      console.log('HTTP request completed', url);
    }
  ): void {
    let headers = new HttpHeaders({
      LanguageCode:
        data?.languageCode || localStorage.getItem('languageCode')?.toString()!,
      Authorization: `Bearer ${localStorage.getItem('token')}`,
    });
    this.http.post<T>(url, data, { headers: headers }).subscribe({
      next: onNext,
      error: onError,
      complete: onComplete,
    });
  }
  public get<T>(
    url: string,
    params: any,
    takeUntil$: Observable<void>,
    onNext: (data: T) => void,
    onError: (error: HttpErrorResponse) => void = async (err) => {
      console.log(err);
    },
    onComplete: () => void = () => {
      console.log('HTTP request completed', url);
    }
  ): void {
    let headers = new HttpHeaders({
      LanguageCode: localStorage.getItem('languageCode')?.toString()!,
      Authorization: `Bearer ${localStorage.getItem('token')}`,
    });
    this.http.get<T>(url, { params, headers: headers }).subscribe({
      next: onNext,
      error: onError,
      complete: onComplete,
    });
  }
}
