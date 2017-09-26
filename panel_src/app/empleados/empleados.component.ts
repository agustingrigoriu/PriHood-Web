import { Component, OnInit } from '@angular/core';
import { EmpleadosService } from './empleados.service';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { NgForm } from "@angular/forms/forms";
import { Empleado } from './empleado.model';
import { NotificationsService } from 'angular2-notifications';

@Component({
  selector: 'app-empleados',
  templateUrl: './empleados.component.html',
  styleUrls: ['./empleados.component.css']
})

export class EmpleadosComponent implements OnInit {
  constructor(protected EmpleadosService: EmpleadosService, private notificaciones: NotificationsService) { }

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
  empleadoSeleccionado: Empleado;

  borrarEmpleado(empleado: Empleado): void {
    console.log(empleado.id_empleado);
    if (confirm('¿Borrar este empleado?')) {
      this.EmpleadosService.deleteEmpleado(empleado.id_empleado).then(response => {
        if (response.error) {
          this.notificaciones.error("Error", "No se pudo borrar el empleado");
        } else {
          this.actualizarListado();
          this.notificaciones.success("Éxito", "Se borró correctamente el empleado");
        }
      });
    }
  }

  crearEmpleado(empleado: any, form: NgForm) {
    this.EmpleadosService.crearEmpleado(empleado).then(response => {
      if (response.error) {
        this.notificaciones.error("Error", "No se pudo crear el empleado");
      } else {
        this.actualizarListado();
        form.reset();
        this.notificaciones.success("Éxito", "Se creó correctamente el empleado");
      }
    });
  }

  onSelectedEmp(empleado: Empleado) {
    console.log(empleado.id_empleado);
    this.empleadoSeleccionado = { ...empleado };
  }

  modificarEmpleado(empleado: Empleado) {
    console.log(empleado.id_empleado);
    console.log(empleado.nombre);
    this.EmpleadosService.updateEmpleado(empleado.id_empleado, empleado).then(response => {
      if (response.error) {
        this.notificaciones.error("Error", "No se pudo modificar el empleado");
      } else {
        this.notificaciones.success("Éxito", "Se modificó correctamente el empleado");
        this.actualizarListado();
      }
    });
  }
  
  actualizarListado() {
    this.EmpleadosService.getAllEmpleados().then(response => {
      this.empleados = response.data;
      console.log(this.empleados);
    });
  }

  ngOnInit(): void {
    this.actualizarListado();
  }

}
