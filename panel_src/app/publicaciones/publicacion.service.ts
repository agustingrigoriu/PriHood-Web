import { Injectable } from '@angular/core';
import { ApiRequestService } from '../../services/api.request.service';
import { Publicacion } from './publicacion.model';

@Injectable()
export class PublicacionesService {
  constructor(protected request: ApiRequestService) { }

  getPublicaciones() {
    return this.request.get<Publicacion[]>(`publicaciones/listado`);
  }

  crearPublicacion(publicacion: Publicacion) {
    return this.request.post<Publicacion>(`publicaciones/publicar`, publicacion);
  }

}