import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/toPromise';

@Injectable()
export class ApiRequestService {
  constructor(protected http: HttpClient) { }

  public BASE_URL = 'http://localhost:5000';

  post<T>(api, data?, params?: HttpParams): Promise<Request<T>> {
    return this.http.post<Request<T>>(`${this.BASE_URL}/api/${api}`, data, { params }).toPromise();
  }

  put<T>(api, data?, params?: HttpParams): Promise<Request<T>> {
    return this.http.put<Request<T>>(`${this.BASE_URL}/api/${api}`, data, { params }).toPromise();
  }

  delete<T>(api, params?: HttpParams): Promise<Request<T>> {
    return this.http.delete<Request<T>>(`${this.BASE_URL}/api/${api}`, { params }).toPromise();
  }

  get<T>(api, params?: HttpParams): Promise<Request<T>> {
    return this.http.get<Request<T>>(`${this.BASE_URL}/api/${api}`, { params }).toPromise();
  }
}

export interface Request<T> {
  error: Boolean,
  data: T
}