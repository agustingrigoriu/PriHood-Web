import { Injectable } from '@angular/core';
import { ApiRequestService } from '../../services/api.request.service';
import { Residencia } from '../residencias/residencia.model'

@Injectable()
export class ExpensasService {
    constructor(protected request: ApiRequestService) { }

    getAllResidencias() {
        return this.request.get<Residencia[]>(`residencias`);
    }
}