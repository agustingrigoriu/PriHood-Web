import { Injectable } from '@angular/core';
import { ApiRequestService } from '../../services/api.request.service';
import { Amenity } from './amenity.model';

@Injectable()
export class AmenitiesService {
  constructor(protected request: ApiRequestService) { }

  getAllAmenities() {
    return this.request.get<Amenity[]>('amenities');
  }

  getAmenity(id: Number) {
    return this.request.get<Amenity>(`amenities/${id}`);
  }

  deleteAmenity(id: Number) {
    return this.request.delete<any>(`amenities/${id}`);
  }

  crearAmenity(amenity: Amenity) {
    return this.request.post<any>(`amenities`, amenity);
  }

  updateAmenity(id:Number, amenity:Amenity) {
    return this.request.put<any>(`amenities/${id}`, amenity);
  }
}