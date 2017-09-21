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

  crearBarrio(barrio: Barrio, usuario: any) {
    return this.request.post<any>(`barrios`, { barrio: barrio, usuario: usuario });
  }

  updateBarrio(id:Number, barrio:Barrio) {
    return this.request.put<any>(`barrios/${id}`, barrio);
  }
}