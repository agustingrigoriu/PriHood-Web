import { Injectable } from '@angular/core';
import { ApiRequestService } from '../../services/api.request.service';
import {Empleado} from './empleado.model';

@Injectable()
export class EmpleadosService {
  constructor(protected request: ApiRequestService) { }

  getAllEmpleados() {
    return this.request.get<any[]>('empleados');
  }

  getEmpleado(id: Number) {
    return this.request.get<any>(`empleados/${id}`);
  }

  deleteEmpleado(id: Number) {
    return this.request.delete<any>(`empleados/${id}`);
  }

  crearEmpleado(empleado: any) {
    return this.request.post<any>(`empleados`, empleado);
  }

  updateEmpleado(id:Number, empleado:Empleado) {
    return this.request.put<any>(`empleados/${id}`, empleado);
  }
}