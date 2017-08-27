import { Component, OnInit } from '@angular/core';
import { EmpleadosService } from './empleados.service';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';

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

  borrarEmpleado(empleado: any): void {
    if (confirm('¿Borrar este usuario?')) {
      this.EmpleadosService.deleteEmpleado(empleado.id).then(response => {
        if (response.error) {
          this.mensaje = 'No se pudo borrar.';
        } else {
          this.actualizarListado();
          this.mensaje = 'Borrado correctamente.';
        }
      });
    }
  }

  crearEmpleado(empleado: any) {
    this.EmpleadosService.crearEmpleado(empleado).then(response => {
      if (response.error) {
        this.mensaje = 'No se pudo crear.';
      } else {
        this.actualizarListado();
        this.mensaje = 'Creado correctamente.';
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

  openBorrar(empleadoborrado) {
    this.modalService.open(empleadoborrado, { windowClass: 'in' });
  }

  ngOnInit(): void {
    this.actualizarListado();
  }

}
