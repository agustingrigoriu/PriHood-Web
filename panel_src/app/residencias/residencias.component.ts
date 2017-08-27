import { Component, OnInit } from '@angular/core';

import { ResidenciasService } from './residencias.service';
import { Residencia } from './residencia.model';

import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-residencias',
  templateUrl: './residencias.component.html',
  styleUrls: ['./residencias.component.css']
})

export class ResidenciaComponent implements OnInit {
  constructor(protected ResidenciasService: ResidenciasService, private modalService: NgbModal) { }

  mensaje: string;

  residencias: Residencia[] = [];

  residencia: Residencia = {
    nombre: '',
    ubicacion: ''
  };
  headerClass:string="alert-info";

  agregarResidencia(residencia: Residencia) {
    this.ResidenciasService.crearResidencia(residencia).then(response => {
      if (response.error) {
        this.mensaje = 'No se pudo crear la residencia.';
        this.headerClass="alert-danger";
      } else {
        this.mensaje = 'Se creo correctamente.';
        this.headerClass="alert-success";
        this.actualizar();
      }
    });
  }

  borrarResidencia(res: Residencia) {
    if (confirm('¿Borrar esta residencia?')) {
      this.ResidenciasService.deleteResidencia(res.id).then(response => {
        if (response.error) {
          this.mensaje = 'No se pudo borrar.';
          this.headerClass="alert-danger";
        } else {
          this.mensaje = 'Borrado correctamente.';
          this.actualizar();
          this.headerClass="alert-success";
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

  open(rescreada) {
    this.modalService.open(rescreada, { windowClass: 'in' })
      ;
  }

  openBorrar(residenciaborrada) {
    this.modalService.open(residenciaborrada, { windowClass: 'in' });
  }

  ngOnInit(): void {
    this.actualizar();
  }
}
