import { Moment } from 'moment';

export interface Alerta {
  id_alerta?: number,
  fecha?: Date | Moment,
  descripcion?: string,
  tipo_alerta?: string,
  id_tipo_alerta?: number,
  id_residente?: number,
  residente?: string
}