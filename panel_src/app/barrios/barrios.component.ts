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

  barrios: Barrio[] = [];

  borrarUsuario(barrio: Barrio): void {
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

  crearEjemplo() {
    let barrio: Barrio = {
      nombre: 'La Lucinda',
      ubicación: 'Av. 23 de Agosto 5467',
    };

    this.BarriosService.crearBarrio(barrio).then(response => {
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
