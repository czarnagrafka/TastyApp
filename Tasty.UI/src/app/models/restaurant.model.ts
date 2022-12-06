import { Cuisine } from "./cuisine.model";

export interface Restaurant {
  id: number;
  name: string;
  latitude: number;
  longitude: number;
  radius: number;
  cuisines: Cuisine[];
}
