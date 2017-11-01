import { Component, OnInit } from '@angular/core';
import { LoginService } from '../../services/login.service';
import { Usuario } from '../usuarios/usuario.model';
import { RootDashboard, AdminDashboard } from './home.model';
import { DashboardService } from './home.service';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})

export class HomeComponent implements OnInit {
  constructor(protected LoginService: LoginService, protected DashboardService: DashboardService) { }
  title = 'Home';
  mensaje: string;
  headerClass: string = " ";

  usuario: Usuario = {
    email: '',
    idPerfil: -1,
    nombre: '',
    apellido: '',
    perfil: '',
    barrio: ''
  };

  rootDashboard: RootDashboard = {
    cantidad_barrios: 0
  };

  adminDashboard: AdminDashboard = {
    cantidad_residencias: 0,
    cantidad_residentes: 0,
    latitud: -31.335335,
    longitud: -64.303113
  };

  getUsuario() {
    this.LoginService.getUserLogin().then(response => {
      if (response.error) {
        this.mensaje = 'No se pudo traer el usuario.';
        this.headerClass = "alert-danger";
      } else {
        this.usuario = response.data;

        if (this.usuario.idPerfil === 1) {
          this.getRootDashboard()
        }

        if (this.usuario.idPerfil === 2) {
          this.getAdminDashboard()
        }
      }
    });
  }

  getRootDashboard() {
    this.DashboardService.getRootDashboard().then(response => {
      if (response.error) {
        this.mensaje = 'No se pudo traer datos de dashboard.';
        this.headerClass = "alert-danger";
      } else {
        this.rootDashboard = response.data;
      }
    });
  }

  getAdminDashboard() {
    this.DashboardService.getAdminDashboard().then(response => {
      if (response.error) {
        this.mensaje = 'No se pudo traer datos de dashboard.';
        this.headerClass = "alert-danger";
      } else {
        this.adminDashboard = response.data;
      }
    });
  }


  ngOnInit() {

    this.getUsuario();


  }

}
