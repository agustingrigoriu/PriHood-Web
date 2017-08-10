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

  borrarResidencia(residencia: Residencia): void {
    if (confirm('¿Borrar esta Residencia?')) {
      this.ResidenciasService.deleteResidencia(residencia.id).then(response => {
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
    let residencia: Residencia = {
       id: 1,
       ubicación: 'Manzana1, Lote1'
    };

    this.ResidenciasService.crearResidencia(residencia).then(response => {
      if (response.error) {
        alert('No se pudo crear.');
      } else {
        this.actualizarListado();
        alert('Creado correctamente.');
      }
    });
  }

  actualizarListado() {
    this.ResidenciasService.getAllResidencias().then(response => {
      this.residencias = response.data;
    });
  }

  ngOnInit(): void {
    this.actualizarListado();
  }
}
