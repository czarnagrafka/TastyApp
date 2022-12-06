import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { Restaurant } from '../models/restaurant.model';

@Injectable({
  providedIn: 'root'
})
export class RestaurantsService {

  constructor(private http: HttpClient) {
  }

  public getAllRestaurants(): Observable<Restaurant[]> {
    return this.http.get<Restaurant[]>('/api/restaurants').pipe(map(response => response ?? []));
  }
}
