import { Component, OnInit } from '@angular/core';
import { UsuariosService } from './usuarios.service';
import { Usuario } from './usuario.model';

@Component({
  selector: 'app-usuarios',
  templateUrl: './usuarios.component.html',
  styleUrls: ['./usuarios.component.css']
})

export class UsuariosComponent implements OnInit {
  constructor(protected UsuariosService: UsuariosService) { }

  usuarios: Usuario[] = [];
  doctipos = [
    {name:'DNI'},
    {name:'Pasaporte'},
    {name:'Visa'},
   
];
  perfiles = [
    {name:'Encargado de Seguridad'},
    {name:'Administrador'},
    {name:'Otro'},
   
];

  borrarUsuario(usuario: Usuario): void {
    if (confirm('¿Borrar este usuario?')) {
      this.UsuariosService.deleteUsuario(usuario.id).then(response => {
        if (response.error) {
          alert('No se pudo borrar.');
        } else {
          this.actualizarListado();
          alert('Borrado correctamente.');
        }
      });
    }
  }

  crearEjemplo() {
    let usuario: Usuario = {
      avatar: 'no',
      email: 'nose@micorreo.com',
      idPerfil: 2,
      nombreUsuario: 'probandoesto',
      password: 'mipassword123$'
    };

    this.UsuariosService.crearUsuario(usuario).then(response => {
      if (response.error) {
        alert('No se pudo crear.');
      } else {
        this.actualizarListado();
        alert('Creado correctamente.');
      }
    });
  }

  actualizarListado() {
    this.UsuariosService.getAllUsuarios().then(response => {
      this.usuarios = response.data;
    });
  }

  ngOnInit(): void {
    this.actualizarListado();
  }
}
