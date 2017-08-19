import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { BarriosService } from '../barrios/barrios.service';
import { Barrio } from '../barrios/barrio.model';


import { ResidenciasService } from './residencias.service';
import { Residencia } from './residencia.model';

@Component({
  selector: 'app-residencias',
  templateUrl: './residencias.component.html',
  styleUrls: ['./residencias.component.css']
})

export class ResidenciaComponent implements OnInit, OnDestroy {
  constructor(protected router: Router, protected route: ActivatedRoute, protected ResidenciasService: ResidenciasService, protected BarriosService: BarriosService) { }

  sub: any;

  residencias: Residencia[] = [];

  residencia: Residencia = {
    nombre: '',
    ubicacion: ''
  };

  barrio: Barrio = {
    nombre: '',
    ubicacion: ''
  };

  agregarResidencia(res: Residencia) {
    const residencia: Residencia = {
      nombre: res.nombre,
      ubicacion: res.ubicacion,
      idBarrio: this.barrio.id
    };

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
    this.ResidenciasService.getAllResidencias(this.barrio.id).then(response => {
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
    this.sub = this.route.params.subscribe(params => {
      const id = +params['barrio'];

      this.BarriosService.getBarrio(id).then(response => {
        if (response.error || !response.data) {
          alert('El barrio no existe.');
          return this.router.navigate(['/barrios']);
        }

        this.barrio = response.data;
        this.actualizar();
      });
    });
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }
}
