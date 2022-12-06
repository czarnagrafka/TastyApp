import { Component } from '@angular/core';
import { faUtensils, faPlus } from '@fortawesome/free-solid-svg-icons';
import { Observable } from 'rxjs';
import { Cuisine } from './models/cuisine.model';
import { CuisinesService } from './services/cuisines.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  public cuisines$: Observable<Cuisine[]>;
  public selectedId = 0;
  public badgeIcon = faUtensils;
  public plusIcon = faPlus;
  public isInputVisible = false;
  public newCuisine = '';

  constructor(private cuisinesService: CuisinesService) {
    this.cuisines$ = cuisinesService.getAllCuisines();
  }

  onChangeFilter(id: number): void {
    this.selectedId = id;
  }

  async onAddCuisine(): Promise<void> {
    if (this.newCuisine.length > 0) {
      this.cuisinesService.addCuisine(this.newCuisine).subscribe({
        next(cuisine) {
          console.log(cuisine);
        },
        error(err) {
          console.error(err);
        },
      });
      this.isInputVisible = false;
      this.newCuisine = '';
      this.cuisines$ = this.cuisinesService.getAllCuisines();
    }
  }
}
