import { Component, OnInit } from '@angular/core';
import { EmpleadosService } from './empleados.service';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { NgForm } from "@angular/forms/forms";
import { Empleado } from './empleado.model';

@Component({
  selector: 'app-empleados',
  templateUrl: './empleados.component.html',
  styleUrls: ['./empleados.component.css']
})

export class EmpleadosComponent implements OnInit {
  constructor(protected EmpleadosService: EmpleadosService, private modalService: NgbModal) { }

  mensaje: string;

  empleado = {
    nombre: '',
    apellido: '',
    id_tipo_documento: '',
    numero_documento: '',
    telefono: '',
    fecha_nacimiento: '',
    password: '',
    id_perfil: '',
  };
  empleados = [];
  doctipos = [
    { name: 'Documento unico', id: 1 },
    { name: 'Libreta de enrolamiento', id: 2 },
    { name: 'Libreta Cívica', id: 3 },
    { name: 'Otros', id: 4 }
  ];
  perfiles = [
    { nombre: 'Administrador', id: 2 },
    { nombre: 'Encargado de Seguridad', id: 4 }
  ];
  headerClass: string = " ";
  empleadoSeleccionado: Empleado;

  borrarEmpleado(empleado: Empleado): void {
    console.log(empleado.id);
    if (confirm('¿Borrar este empleado?')) {
      this.EmpleadosService.deleteEmpleado(empleado.id).then(response => {
        if (response.error) {
          this.mensaje = 'No se pudo borrar.';
          this.headerClass = "alert-danger";
        } else {
          this.actualizarListado();
          this.mensaje = 'Borrado correctamente.';
          this.headerClass = "alert-success";
        }
      });
    }
  }

  crearEmpleado(empleado: any, form: NgForm) {
    this.EmpleadosService.crearEmpleado(empleado).then(response => {
      if (response.error) {
        this.mensaje = 'No se pudo crear.';
        this.headerClass = "alert-danger";
      } else {
        this.actualizarListado();
        form.reset();
        this.mensaje = 'Creado correctamente.';
        this.headerClass = "alert-success";
      }
    });
  }

  onSelectedEmp(empleado: Empleado) {
    console.log(empleado.id);
    this.empleadoSeleccionado = { ...empleado };
  }

  modificarEmpleado(empleado: Empleado) {
    this.EmpleadosService.updateEmpleado(empleado.id, empleado).then(response => {
      if (response.error) {
        this.mensaje = 'No se pudo modificar el empleado.';
        this.headerClass = "alert-danger";
      } else {
        this.mensaje = 'Se modificó correctamente.';
        this.headerClass = "alert-success";
        this.actualizarListado();
      }
    });
  }
  actualizarListado() {
    this.EmpleadosService.getAllEmpleados().then(response => {
      this.empleados = response.data;
    });
  }

  open(empleadocreado) {
    this.modalService.open(empleadocreado, { windowClass: 'in' });
  }

  ngOnInit(): void {
    this.actualizarListado();
  }

}
