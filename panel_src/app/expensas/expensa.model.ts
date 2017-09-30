import { Moment } from 'moment';

export interface Expensa {
  id?: number,
  fecha_expensa?: Date | Moment,
  fecha_transaccion?: Date | Moment,
  fecha_vencimiento?: Date | Moment,
  monto?: number,
  pagado?: boolean,
  observaciones?: string,
  url_expensa?: string,
  id_residencia?: number,
  residencia?: string
}