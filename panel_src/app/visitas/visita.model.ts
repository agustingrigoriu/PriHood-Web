export interface Visita {
    id: number;
    apellido: string;
    avatar: string;
    fecha_visita?: string;
    documento?: string;
    tipo_visita: string;
    nombre: string;
    numero_documento?: number;
    observaciones: string;
    patente: string;
    estado: string;
}