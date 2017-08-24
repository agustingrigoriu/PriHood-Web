import { Injectable } from '@angular/core';
import { ApiRequestService } from '../../services/api.request.service';
import { Visitante } from './visitante.model';

@Injectable()
export class VisitantesService {
    constructor(protected request: ApiRequestService) { }

    getAllVisitantes() {
        return this.request.get<Visitante[]>(`visitantes`);
    }

    getVisitante(id: Number) {
        return this.request.get<Visitante>(`visitantes/${id}`);
    }

}