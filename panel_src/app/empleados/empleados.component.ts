
import { Component, OnInit } from '@angular/core';
import { EmpleadosService } from './empleados.service';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { NgForm } from "@angular/forms/forms";
import { Empleado } from './empleado.model';
import { NotificationsService } from 'angular2-notifications';
import { ConfirmationService } from '@jaspero/ng2-confirmations';
import { Validators } from '@angular/forms';
import { FormControl } from '@angular/forms';
import { FormGroup } from '@angular/forms';



@Component({
  selector: 'app-empleados',
  templateUrl: './empleados.component.html',
  styleUrls: ['./empleados.component.css']
})

export class EmpleadosComponent implements OnInit {
  empForm: any;
  constructor(protected EmpleadosService: EmpleadosService, private notificaciones: NotificationsService, private confirmacion: ConfirmationService) { }

  empleado = {
    nombre: '',
    apellido: '',
    id_tipo_documento: '',
    numero_documento: '',
    telefono: '',
    fecha_nacimiento: '',
    password: '',
    confirmPassword:'',
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
    this.EmpleadosService.deleteEmpleado(empleado.id_empleado).then(response => {
      if (response.error) {
        this.notificaciones.error("Error", "No se pudo borrar el empleado");
      } else {
        this.notificaciones.success("Éxito", "Se borró correctamente el empleado");
        this.actualizarListado();
      }
    });
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
    this.empleadoSeleccionado = { ...empleado };
  }

  modificarEmpleado(empleado: Empleado) {
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
    });
  }

  ngOnInit(): void {
    this.actualizarListado();
    this.empForm = new FormGroup({
      'name': new FormControl(this.empleado.nombre, [
        Validators.required,
      ]),

    });
  }

  confirmacionEliminar(empleado: Empleado) {
    this.confirmacion.create('Confirmación', '¿Está seguro qué desea borrar?', { showCloseButton: true, confirmText: "SI", declineText: "NO" })
      .subscribe((ans: any) => {
        if (ans.resolved) {
          this.borrarEmpleado(empleado);
        }
      });
  }
}
