import { Injectable } from '@angular/core';
import { ApiRequestService } from './api.request.service';
import { Usuario } from '../app/usuarios/usuario.model';

@Injectable()
export class LoginService {
  constructor(protected request: ApiRequestService) { }

  getUserLogin() {
    return this.request.get<Usuario>('login');
  }
}
