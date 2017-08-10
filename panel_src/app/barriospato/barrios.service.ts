import { Injectable } from '@angular/core';
import { ApiRequestService } from '../../services/api.request.service';
import { Barrio } from './barrio.model';

@Injectable()
export class BarriosService {
  constructor(protected request: ApiRequestService) { }

  getAllBarrios() {
    return this.request.get<Barrio[]>('barrios');
  }

  getBarrio(id: Number) {
    return this.request.get<Barrio>(`barrios/${id}`);
  }

  deleteBarrio(id: Number) {
    return this.request.delete<any>(`barrios/${id}`);
  }

  crearBarrio(barrio: Barrio) {
    return this.request.post<any>(`barrios`, barrio);
  }

  updateBarrio(barrio: Barrio) {
    return this.request.put<any>(`barrios/${barrio.id}`, barrio);
  }
}