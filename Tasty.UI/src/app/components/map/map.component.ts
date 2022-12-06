import { AfterViewInit, Component, Input, OnDestroy, SimpleChanges } from "@angular/core";
import * as L from 'leaflet';

import { RestaurantsService } from '../../services/restaurants.service';
import { Restaurant } from '../../models/restaurant.model';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

const iconRetinaUrl = 'assets/marker-icon-2x.png';
const iconUrl = 'assets/marker-icon.png';
const shadowUrl = 'assets/marker-shadow.png';
const iconDefault = L.icon({
  iconRetinaUrl,
  iconUrl,
  shadowUrl
});
L.Marker.prototype.options.icon = iconDefault;

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})
export class MapComponent implements AfterViewInit, OnDestroy {
  private map: any;
  private markers = [] as [Restaurant, L.Marker, L.Circle][];
  private destroy$ = new Subject<void>();

  @Input()
  set filter(id: number) {
    this.markers.forEach(([restaurant, marker, circle]) => {
      if (restaurant.cuisines.some(cuisine => id === cuisine.id) || id === 0) {
        marker.addTo(this.map);
        circle.addTo(this.map);
      } else {
        this.map.removeLayer(marker);
        this.map.removeLayer(circle);
      }
    });
  };

  constructor(private restaurantService: RestaurantsService) { }

  ngAfterViewInit(): void {
    this.initMap();
    this.makeRestaurantMarkers();
    this.map.on('click', (e: L.LeafletMouseEvent) => this.onMapClick(e));
  }

  ngOnDestroy(): void {
    this.destroy$.next();  // trigger the unsubscribe
    this.destroy$.complete(); // finalize & clean up the subject stream
  }

  private initMap(): void {
    this.map = L.map('map', {
      center: [50.0584, 19.9407],
      zoom: 15
    });

    const tiles = L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
      maxZoom: 18,
      minZoom: 3,
      attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
    });

    tiles.addTo(this.map);
  }

  private makeRestaurantMarkers(): void {
    this.restaurantService.getAllRestaurants()
      .pipe(takeUntil(this.destroy$))
      .subscribe((restaurants: Restaurant[]) => {
        restaurants.forEach(restaurant => {
          const marker = L.marker([restaurant.latitude, restaurant.longitude])
            .bindPopup(this.makeRestaurantPopup(restaurant));
          marker.addTo(this.map);

          const circle = L.circle([restaurant.latitude, restaurant.longitude],
            {
              color: 'blue',
              fillOpacity: 0.3,
              radius: restaurant.radius
            }).addTo(this.map);

          this.markers.push([restaurant, marker, circle]);
        });

      });
  }

  private makeRestaurantPopup(restaurant: Restaurant): string {
    let popup = '<div>';
    restaurant.cuisines.forEach(cuisine => popup += `<span class="badge bg-info mb-2">${cuisine.name}</span>`);
    popup += `</div><div><strong>${restaurant.name}</strong></div>`;
    return popup;
  }

  private onMapClick(e: L.LeafletMouseEvent) {
    const restaurants = [] as string[];
    this.markers.forEach(([restaurant, marker]) => {
      const distance = marker.getLatLng().distanceTo(e.latlng);
      if (distance < restaurant.radius)
        restaurants.push(restaurant.name)
    });

    const message = restaurants.length > 0
      ? `Dostępne lokale: ${restaurants.join(", ")}`
      : 'Nie znaleziono lokali z dowozem w tym rejonie.';

    L.popup()
      .setLatLng(e.latlng)
      .setContent(message)
      .openOn(this.map);
  }
}

