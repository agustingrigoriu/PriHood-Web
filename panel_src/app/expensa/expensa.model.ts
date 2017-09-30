export interface Expensa {
    id_expensas: number,
    fecha_expensa: Date,
    fecha_transaccion: Date,
    fecha_vencimiento: Date,
    monto: number,
    pagado: boolean,
    observaciones: string,
    url_expensa: string,
    id_residencia: number,
    residencia: string
}