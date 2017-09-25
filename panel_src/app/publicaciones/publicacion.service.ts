import { Injectable } from '@angular/core';
import { ApiRequestService } from '../../services/api.request.service';
import { Publicacion } from './publicacion.model';

@Injectable()
export class PublicacionesService {
  constructor(protected request: ApiRequestService) { }

  getPublicaciones() {
    return this.request.get<Publicacion[]>(`publicaciones/listado`);
  }

  getPublicacion(id: number) {
    return this.request.get<Publicacion>(`publicaciones/${id}`);
  }

  crearPublicacion(publicacion: Publicacion) {
    return this.request.post<Publicacion>(`publicaciones/publicar`, publicacion);
  }

}