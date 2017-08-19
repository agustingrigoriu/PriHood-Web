import { Component, OnInit } from '@angular/core';
import { BarriosService } from './barrios.service';
import { Barrio } from './barrio.model';

@Component({
  selector: 'app-barrios',
  templateUrl: './barrios.component.html',
  styleUrls: ['./barrios.component.css']
})

export class BarriosComponent implements OnInit {
  constructor(protected BarriosService: BarriosService) { }

  usuario = {
    nombre: '',
    apellido: '',
    id_tipo_documento: '',
    numero_documento: '',
    telefono: '',
    fecha_nacimiento: '',
    password: '',
  };

  barrios: Barrio[] = [];
  barrio: Barrio = {
    nombre: '',
    ubicacion: ''
  };

  doctipos = [
    { name: 'Documento único', id: 1 },
    { name: 'Libreta de enrolamiento', id: 2 },
    { name: 'Libreta Cívica', id: 3 },
    { name: 'Otros', id: 4 }
  ];

  borrarBarrio(barrio: Barrio): void {
    if (confirm('¿Borrar este barrio?')) {
      this.BarriosService.deleteBarrio(barrio.id).then(response => {
        if (response.error) {
          alert('No se pudo borrar.');
        } else {
          this.actualizarListado();
          alert('Borrado correctamente.');
        }
      });
    }
  }

  crearBarrio(barrio: Barrio, usuario: any) {
    this.BarriosService.crearBarrio(barrio, usuario).then(response => {
      if (response.error) {
        alert('No se pudo crear.');
      } else {
        this.actualizarListado();
        alert('Creado correctamente.');
      }
    });
  }

  actualizarListado() {
    this.BarriosService.getAllBarrios().then(response => {
      this.barrios = response.data;
    });
  }

  ngOnInit(): void {
    this.actualizarListado();
  }
}
