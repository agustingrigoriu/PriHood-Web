import { Visitante } from '../visitantes/visitante.model';

export interface Visitante {
    id?: number;
    apellido: string;
    nombre: string;
    // avatar?: string;
    // fechaVisita: string;
    tipo_documento: number;
    tipo_visita:string;
    numero_documento:number;
    // telefono?: number;
    observaciones?: string
    patente?: number;
    estado:string;
}