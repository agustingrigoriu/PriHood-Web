import { Injectable } from '@angular/core';
import { ApiRequestService } from '../../services/api.request.service';
import { Publicacion } from './publicacion.model';
import { Comentario } from './comentario.model';

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

  getComentarios(id_publicacion: number) {
    return this.request.get<Comentario[]>(`publicaciones/comentarios/${id_publicacion}`);
  }

  comentar(id_publicacion: number, comentario) {
    return this.request.post<any>(`publicaciones/${id_publicacion}/comentar`, comentario);
  }

  borrarPublicacion(id_publicacion: number) {
    return this.request.delete<any>(`publicaciones/${id_publicacion}/borrar`);
  }
}