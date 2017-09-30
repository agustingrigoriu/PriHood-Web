import { Injectable } from '@angular/core';
import { ApiRequestService } from '../../services/api.request.service';
import { Residencia } from '../residencias/residencia.model'

@Injectable()
export class ExpensaService {
    constructor(protected request: ApiRequestService) { }

    getAllResidencias() {
        return this.request.get<Residencia[]>(`residencias`);
    }

    getResidencia(id: Number) {
        return this.request.get<Residencia>(`residencias/${id}`);
    }

    getExpensas(id: Number) {
        return this.request.get<any>(`expensas/residencias/${id}`);
    }
}