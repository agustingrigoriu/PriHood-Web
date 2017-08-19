import { Injectable } from '@angular/core';
import { ApiRequestService } from '../../services/api.request.service';
import { Visita } from './visita.model';

@Injectable()
export class VisitasService {
    constructor(protected request: ApiRequestService) { }

    getAllVisitas() {
        return this.request.get<Visita[]>(`visitas`);
    }

    getVisita(id: Number) {
        return this.request.get<Visita>(`visitas/${id}`);
    }

}