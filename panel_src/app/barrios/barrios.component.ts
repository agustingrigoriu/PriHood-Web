import { Component, OnInit } from '@angular/core';
import { BarriosService } from './barrios.service';
import { Barrio } from './barrio.model';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { NgForm } from "@angular/forms/forms";

@Component({
  selector: 'app-barrios',
  templateUrl: './barrios.component.html',
  styleUrls: ['./barrios.component.css']
})

export class BarriosComponent implements OnInit {
  constructor(protected BarriosService: BarriosService, private modalService: NgbModal) { }

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
  mensaje: string;
  mensajeBorrado: string;
  headerClass = " "
  barrioSeleccionado: Barrio;


  borrarBarrio(barrio: Barrio): void {
     console.log(barrio.id);
    if (confirm('¿Borrar este barrio?')) {
      this.BarriosService.deleteBarrio(barrio.id).then(response => {
        if (response.error) {
          this.mensajeBorrado = "No se pudo borrar";
        } else {
          this.actualizarListado();
          this.mensajeBorrado = "No se pudo borrar";
        }
      });
    }
  }

  crearBarrio(barrio: Barrio, usuario: any, form: NgForm) {
    this.BarriosService.crearBarrio(barrio, usuario).then(response => {
      if (response.error) {
        this.mensaje = "No se pudo crear";
        this.headerClass = "alert-danger";
      } else {
        this.actualizarListado();
        form.reset();
        this.mensaje = "Se creo correctamente";
        this.headerClass = "alert-success";
      }
    });
  }

  onSelectedBar(barrio: Barrio) {
    this.barrioSeleccionado = { ...barrio };
  }

  modificarBarrio(barrio: Barrio) {
    this.BarriosService.updateBarrio(barrio.id, barrio).then(response => {
      if (response.error) {
        this.mensaje = 'No se pudo modificar el barrio.';
        this.headerClass = "alert-danger";
      } else {
        this.mensaje = 'Se modificó correctamente.';
        this.headerClass = "alert-success";
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

  open(barriocreado) {
    this.modalService.open(barriocreado, { windowClass: 'in' });
  }

  openBorrar(barrioborrado) {
    this.modalService.open(barrioborrado, { windowClass: 'in' });
  }
}
