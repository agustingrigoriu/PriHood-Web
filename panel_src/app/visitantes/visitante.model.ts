import { Visitante } from '../visitantes/visitante.model';

export interface Visitante {
    id?: number;
    apellido: string;
    nombre: string;
    avatar?: string;
    fechaVisita: string;
    idTipoDocumento: number;
    idTipoVisita:number;
    numeroDocumento:number;
    telefono?: number;
    Observaciones?: string
    patente?: number;
}