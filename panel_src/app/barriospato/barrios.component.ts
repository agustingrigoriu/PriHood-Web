import { Component, OnInit } from '@angular/core';
import { BarriosService } from './barrios.service';
import { Barrio } from './barrio.model';

@Component({
  selector: 'app-barrios',
  templateUrl: './barrios.component.html',
})

export class BarriosComponent implements OnInit {
  constructor(private BarriosService: BarriosService) { }

  barrios: Barrio[] = [];

  agregarBarrio() {
    const nombre = prompt('Nombre del barrio');
    const ubicacion = prompt('Ubicacion del barrio');

    const barrio: Barrio = {
      nombre: nombre,
      ubicacion: ubicacion
    };

    this.BarriosService.crearBarrio(barrio).then(response => {
      if (response.error) {
        alert('No se pudo crear el barrio.');
      } else {
        alert('Se creo correctamente.');
        this.actualizar();
      }
    });
  }

  actualizar() {
    this.BarriosService.getAllBarrios().then(response => {
      if (response.error) {
        alert('No se pudo cargar los barrios.');
      } else {
        this.barrios = response.data;
      }
    });
  }

  borrarBarrio(barrio: Barrio) {

    if(!confirm('Borrar?')) return;

    this.BarriosService.deleteBarrio(barrio.id).then(response => {
      if (response.error) {
        alert('No se pudo borrar el barrio.');
      } else {
        alert('Se borro correctamente.');
        this.actualizar();
      }
    });
  }

  editarBarrio(barrio: Barrio) {
    let barrioEditado: Barrio = {
      nombre: prompt('Nombre', barrio.nombre),
      ubicacion: prompt('Ubicacion', barrio.ubicacion),
      id: barrio.id
    };

    this.BarriosService.updateBarrio(barrioEditado).then(response => {
      if (response.error) {
        alert('No se pudo modificar el barrio.');
      } else {
        alert('Se modifico correctamente.');
        this.actualizar();
      }
    });
  }

  ngOnInit(): void {
    this.actualizar();
  }
}
