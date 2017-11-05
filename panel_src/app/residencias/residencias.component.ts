import { Component, OnInit } from '@angular/core';
import { ResidenciasService } from './residencias.service';
import { Residencia } from './residencia.model';
import { NgForm } from "@angular/forms/forms";
import { NotificationsService } from 'angular2-notifications';
import { ConfirmationService } from '@jaspero/ng2-confirmations';
import { Validators } from '@angular/forms';
import { FormControl } from '@angular/forms';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-residencias',
  templateUrl: './residencias.component.html',
  styleUrls: ['./residencias.component.css']
})

export class ResidenciaComponent implements OnInit {
  resForm: any;
  constructor(protected ResidenciasService: ResidenciasService, private notificaciones: NotificationsService, private confirmacion: ConfirmationService) { }

  residencias: Residencia[] = [];
  residencia: Residencia = {
    nombre: '',
    ubicacion: ''
  };
  residenciaSeleccionada: Residencia;
  p: number = 1;

  agregarResidencia(residencia: Residencia, form: NgForm) {
    this.ResidenciasService.crearResidencia(residencia).then(response => {
      if (response.error) {
        this.notificaciones.error("Error", "No se pudo crear la residencia");
      } else {
        this.notificaciones.success("Éxito", "Se creó correctamente la residencia");
        this.actualizar();
        form.reset();
      }
    });
  }

  borrarResidencia(res: Residencia) {
    this.ResidenciasService.deleteResidencia(res.id).then(response => {
      if (response.error) {
        this.notificaciones.error("Error", "No se pudo borrar la residencia");
      } else {
        this.notificaciones.success("Éxito", "Se borró correctamente la residencia");
        this.actualizar();
      }
    });
  }

  onSelectedRes(residencia: Residencia) {
    this.residenciaSeleccionada = { ...residencia };
  }

  modificarResidencia(residencia: Residencia) {
    this.ResidenciasService.updateResidencia(residencia.id, residencia).then(response => {
      if (response.error) {
        this.notificaciones.error("Error", "No se pudo modificar la residencia");
      } else {
        this.notificaciones.success("Éxito", "Se modificó correctamente la residencia");
        this.actualizar();
      }
    });
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
    this.resForm = new FormGroup({
      'name': new FormControl(this.residencia.nombre, [
        Validators.required,
      ]),
    });
  }

  confirmacionEliminar(residencia: Residencia) {
    this.confirmacion.create('Confirmación', '¿Está seguro qué desea borrar?', { showCloseButton: true, confirmText: "SI", declineText: "NO" })
      .subscribe((ans: any) => {
        if (ans.resolved) {
          this.borrarResidencia(residencia);
        }
      });
  }
}
