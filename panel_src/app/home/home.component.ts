import { Component, OnInit } from '@angular/core';
import { LoginService } from '../../services/login.service';
import { Usuario } from '../usuarios/usuario.model';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})

export class HomeComponent implements OnInit {
  constructor(protected LoginService: LoginService) { }
  title = 'Home';
  lat: number = -31.335335;
  lng: number = -64.303113;
  mensaje: string;
  headerClass: string = " ";

  public usuario: Usuario = {
    email: '',
    idPerfil: -1,
    nombre: '',
    apellido: '',
    perfil: '',
    barrio: ''
  };

  getUsuario() {
    this.LoginService.getUserLogin().then(response => {
      if (response.error) {
        this.mensaje = 'No se pudo crear la residencia.';
        this.headerClass = "alert-danger";
      } else {
        this.usuario = response.data;
        this.mensaje = 'Se creo correctamente.';
        this.headerClass = "alert-success";
      }
    });
  }


  ngOnInit(): void {
    this.getUsuario();
  }

}
