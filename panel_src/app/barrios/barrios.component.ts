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

  @ViewChild('crearBarrioModal') crearBarrioModal: TemplateRef<NgbModalRef>;
  private modalRef: NgbModalRef;

  constructor(protected BarriosService: BarriosService, private notificaciones: NotificationsService, private modalService: NgbModal, private confirmacion: ConfirmationService) { }

  markerDraggable: boolean = true;
  markerClickable: boolean = true;
  markerLabel: string = "B";
  cargando: boolean = false;

  usuario = {
    nombre: '',
    apellido: '',
    id_tipo_documento: '',
    numero_documento: '',
    telefono: '',
    fecha_nacimiento: '',
    password: '',
    confirmPass: '',
  };

  barrios: Barrio[] = [];
  barrio: Barrio = {
    nombre: '',
    ubicacion: '',
    latitud: -31.335335,
    longitud: -64.303113
  };

  doctipos = [
    { name: 'Documento único', id: 1 },
    { name: 'Libreta de enrolamiento', id: 2 },
    { name: 'Libreta Cívica', id: 3 },
    { name: 'Otros', id: 4 }
  ];
  barrioSeleccionado: Barrio;

  dragEnd($event) {
    this.barrio.latitud = $event.coords.lat;
    this.barrio.longitud = $event.coords.lng;
  }

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

  crearBarrio(barrio: Barrio, usuario: any) {
    this.cargando=true;
    this.BarriosService.crearBarrio(barrio, usuario).then(response => {
      this.cargando=false;
      if (response.error) {
        this.notificaciones.error("Error", "No se pudo crear el barrio");
      } else {
        this.modalRef.close();
        this.actualizarListado();
        this.notificaciones.success("Éxito", "Se creó correctamente el barrio");
      }
    });
  }

  onSelectedBar(barrio: Barrio) {
    this.barrioSeleccionado = { ...barrio };
  }

  modificarBarrio(barrio: Barrio) {
    this.cargando=true;
    this.BarriosService.updateBarrio(barrio.id, barrio).then(response => {
      this.cargando=false;
      if (response.error) {
        this.notificaciones.error("Error", "No se pudo modificar el barrio");
      } else {
        this.notificaciones.success("Éxito", "Se modificó correctamente el barrio");
        this.actualizarListado();
      }
    });
  }

  actualizarListado() {
    this.cargando=true;
    this.BarriosService.getAllBarrios().then(response => {
      this.barrios = response.data;
      this.cargando=false;
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
