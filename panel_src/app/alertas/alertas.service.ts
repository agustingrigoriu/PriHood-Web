import { Injectable } from '@angular/core';
import { ApiRequestService } from '../../services/api.request.service';
import { Alerta } from './alerta.model'

@Injectable()
export class AlertasService {
    constructor(protected request: ApiRequestService) { }

    getAllAlertas() {
        return this.request.get<Alerta[]>(`alertas/alertas_nuevas`);
    }

    marcarVisto(alerta: Alerta) {
        return this.request.post<any>(`alertas/visto/${alerta.id_alerta}`);
    }
}