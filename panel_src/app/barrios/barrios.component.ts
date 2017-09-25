import { Component, OnInit } from '@angular/core';
import { BarriosService } from './barrios.service';
import { Barrio } from './barrio.model';
import { NgForm } from "@angular/forms/forms";
import { NotificationsService } from 'angular2-notifications';

@Component({
  selector: 'app-barrios',
  templateUrl: './barrios.component.html',
  styleUrls: ['./barrios.component.css']
})

export class BarriosComponent implements OnInit {
  constructor(protected BarriosService: BarriosService, private notificaciones: NotificationsService) { }

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
  barrioSeleccionado: Barrio;

  borrarBarrio(barrio: Barrio): void {
    if (confirm('¿Borrar este barrio?')) {
      this.BarriosService.deleteBarrio(barrio.id).then(response => {
        if (response.error) {
          this.notificaciones.error("Error", "No se pudo borrar el barrio");
        } else {
          this.actualizarListado();
          this.notificaciones.success("Éxito", "Se borró correctamente el barrio");
        }
      });
    }
  }

  crearBarrio(barrio: Barrio, usuario: any, form: NgForm) {
    this.BarriosService.crearBarrio(barrio, usuario).then(response => {
      if (response.error) {
        this.notificaciones.error("Error", "No se pudo crear el barrio");
      } else {
        this.actualizarListado();
        form.reset();
        this.notificaciones.success("Éxito", "Se creó correctamente el barrio");
      }
    });
  }

  onSelectedBar(barrio: Barrio) {
    this.barrioSeleccionado = { ...barrio };
  }

  modificarBarrio(barrio: Barrio) {
    this.BarriosService.updateBarrio(barrio.id, barrio).then(response => {
      if (response.error) {
        this.notificaciones.error("Error", "No se pudo modificar el barrio");
      } else {
        this.notificaciones.success("Éxito", "Se modificó correctamente el barrio");
        this.actualizarListado();
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
