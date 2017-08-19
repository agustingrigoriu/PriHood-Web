import { Injectable } from '@angular/core';
import { ApiRequestService } from '../../services/api.request.service';
import { Usuario } from './usuario.model';

@Injectable()
export class UsuariosService {
  constructor(protected request: ApiRequestService) { }

  getAllUsuarios() {
    return this.request.get<Usuario[]>('usuarios');
  }

  getUsuario(id: Number) {
    return this.request.get<Usuario>(`usuarios/${id}`);
  }

  deleteUsuario(id: Number) {
    return this.request.delete<any>(`usuarios/${id}`);
  }

  crearUsuario(usuario: any) {
    return this.request.post<any>(`usuarios`, usuario);
  }
}