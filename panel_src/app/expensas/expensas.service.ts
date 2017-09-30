import { Injectable } from '@angular/core';
import { ApiRequestService } from '../../services/api.request.service';
import { Residencia } from '../residencias/residencia.model'
import { Expensa } from './expensa.model'

@Injectable()
export class ExpensasService {
    constructor(protected request: ApiRequestService) { }

    getAllResidencias() {
        return this.request.get<Residencia[]>(`residencias`);
    }

    cargarExpensa(data, file) {
        return this.request.upload<any>(`expensas`, data, file);
    }
}