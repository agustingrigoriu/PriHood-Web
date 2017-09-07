import { Component, OnInit } from '@angular/core';
import { LoginService } from '../services/login.service';
import { Usuario } from './usuarios/usuario.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit {
  title = 'app';

  constructor(protected LoginService: LoginService) { }

  public usuario: Usuario = {
    email: '',
    idPerfil: -1,
    nombre: '',
    apellido: '',
    perfil: '',
    barrio: ''
  };

  async ngOnInit() {
    try {
      const { error, data: usuario } = await this.LoginService.getUserLogin();

      if (error || !usuario) {
        throw 'Usuario no logueado.';
      }

      this.usuario = usuario;
    } catch (error) {
      window.location.href = '/Login';
    }
  }
}
