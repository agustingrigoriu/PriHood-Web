import { Injectable } from '@angular/core';
import { ApiRequestService } from '../../services/api.request.service';
import { Turno } from './turno.model';

@Injectable()
export class TurnosService {
  constructor(protected request: ApiRequestService) { }

  getAllTurnos(amenity: number) {
    return this.request.get<Turno[]>(`turnos/${amenity}`);
  }

  getTurno(id: Number) {
    return this.request.get<Turno>(`turnos/${id}`);
  }

  deleteTurno(id: Number) {
    return this.request.delete<any>(`turnos/${id}`);
  }

  crearTurno(amenity: number, turno: Turno) {
    return this.request.post<any>(`turnos/${amenity}`, turno);
  }

  actualizarTurno(id: number, turno: Turno) {
    return this.request.put<any>(`turnos/${id}`, turno);
  }

  getAllDias() {
    return this.request.get<any>(`tipos/dias`);
  }
}