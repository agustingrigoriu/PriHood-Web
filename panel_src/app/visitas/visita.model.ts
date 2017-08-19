import { Visita } from '../visitas/visita.model';

export interface Visita {
    id?: number;
    nombre: string;
    apellido: string;
    dni: number;
    patente?: number;
    telefono?: number;
}