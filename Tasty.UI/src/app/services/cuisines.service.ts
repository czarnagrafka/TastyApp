import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { Restaurant } from '../models/restaurant.model';
import { Cuisine } from '../models/cuisine.model';

@Injectable({
  providedIn: 'root',
})
export class CuisinesService {
  constructor(private http: HttpClient) {}

  public getAllCuisines(): Observable<Cuisine[]> {
    return this.http
      .get<Restaurant[]>('/api/cuisines')
      .pipe(map((response) => response ?? []));
  }

  public addCuisine(name: string): Observable<Cuisine> {
    const body = { name };
    return this.http.post<Cuisine>('/api/cuisines', body);
  }
}
