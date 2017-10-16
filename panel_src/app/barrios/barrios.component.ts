import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { BarriosService } from './barrios.service';
import { Barrio } from './barrio.model';
import { NgForm } from "@angular/forms/forms";
import { NotificationsService } from 'angular2-notifications';
import { ConfirmationService } from '@jaspero/ng2-confirmations';

@Component({
  selector: 'app-barrios',
  templateUrl: './barrios.component.html',
  styleUrls: ['./barrios.component.css']
})

export class BarriosComponent implements OnInit {

  @ViewChild('crearBarrio') crearBarrioModal: TemplateRef<NgbModalRef>;
  private modalRef: NgbModalRef;

  constructor(protected BarriosService: BarriosService, private notificaciones: NotificationsService, private modalService: NgbModal, private confirmacion: ConfirmationService) { }


  lat: number = -31.335335;
  lng: number = -64.303113;
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

  abrirModalCrearBarrio() {
    this.modalRef = this.modalService.open(this.crearBarrioModal);
  }

  borrarBarrio(barrio: Barrio): void {
    this.BarriosService.deleteBarrio(barrio.id).then(response => {
      if (response.error) {
        this.notificaciones.error("Error", "No se pudo borrar el barrio");
      } else {
        this.actualizarListado();
        this.notificaciones.success("Éxito", "Se borró correctamente el barrio");
      }
    });
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


  confirmacionEliminar(barrio: Barrio) {
    this.confirmacion.create('Confirmación', '¿Está seguro qué desea borrar?', { showCloseButton: true, confirmText: "SI", declineText: "NO" })
      .subscribe((ans: any) => {
        if (ans.resolved) {
          this.borrarBarrio(barrio);
        }
      });
  }
}
