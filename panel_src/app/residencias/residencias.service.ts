import { Injectable } from '@angular/core';
import { ApiRequestService } from '../../services/api.request.service';
import { Residencia } from './residencia.model';

@Injectable()
export class ResidenciasService {
  constructor(protected request: ApiRequestService) { }

  getAllResidencias() {
    return this.request.get<Residencia[]>('residencias');
  }

  getResidencia(id: Number) {
    return this.request.get<Residencia>(`residencias/${id}`);
  }

  deleteResidencia(id: Number) {
    return this.request.delete<any>(`residencias/${id}`);
  }

  crearResidencia(residencia: Residencia) {
    return this.request.post<any>(`residencias`, residencia);
  }
}