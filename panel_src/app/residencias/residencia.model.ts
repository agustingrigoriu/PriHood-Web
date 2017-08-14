import { Barrio } from '../barrios/barrio.model';

export interface Residencia {
  id?: number;
  nombre: string;
  ubicacion: string;
  codigo?: string;
  idBarrio?: number;
  idBarrioNavigation?: Barrio;
}