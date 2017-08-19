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

  usuario = {
    nombre: '',
    apellido: '',
    id_tipo_documento: '',
    numero_documento: '',
    telefono: '',
    fecha_nacimiento: '',
    password: '',
    id_perfil: '',
  };
  usuarios: Usuario[] = [];
  doctipos = [
    { name: 'Documento único', id: 1 },
    { name: 'Libreta de enrolamiento', id: 2 },
    { name: 'Libreta Cívica', id: 3 },
    { name: 'Otros', id: 4 }
  ];
  perfiles = [
    { nombre: 'Administrador', id: 1 },
    { nombre: 'Encargado de Seguridad', id: 2 }
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

  crearUsuario(usuario: Usuario) {

    console.log(usuario);
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
