import { Component, OnInit } from '@angular/core';

import { ResidenciasService } from './residencias.service';
import { Residencia } from './residencia.model';

@Component({
  selector: 'app-residencias',
  templateUrl: './residencias.component.html',
  styleUrls: ['./residencias.component.css']
})

export class ResidenciaComponent implements OnInit {
  constructor(protected ResidenciasService: ResidenciasService) { }

  residencias: Residencia[] = [];

  residencia: Residencia = {
    nombre: '',
    ubicacion: ''
  };

  agregarResidencia(residencia: Residencia) {
    this.ResidenciasService.crearResidencia(residencia).then(response => {
      if (response.error) {
        alert('No se pudo crear la residencia.');
      } else {
        alert('Se creo correctamente.');
        this.actualizar();
      }
    });
  }

  borrarResidencia(res: Residencia) {
    if (confirm('¿Borrar esta residencia?')) {
      this.ResidenciasService.deleteResidencia(res.id).then(response => {
        if (response.error) {
          alert('No se pudo borrar.');
        } else {
          alert('Borrado correctamente.');
          this.actualizar();
        }
      });
    }
  }

  actualizar() {
    this.ResidenciasService.getAllResidencias().then(response => {
      if (response.error) {
        alert('No se pudo cargar las residencias.');
      } else {
        this.residencias = response.data;
      }
    });
  }

  imprimirListado() {
    window.print();
  }

  ngOnInit(): void {
    this.actualizar();
  }
}
