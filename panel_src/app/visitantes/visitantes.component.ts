import { Component, OnInit } from '@angular/core';
import { VisitantesService } from './visitantes.service';
import { Visitante } from './visitante.model';

@Component({
    selector: 'app-visitantes',
    templateUrl: './visitantes.component.html',
    styleUrls: ['./visitantes.component.css']
})

export class VisitantesComponent implements OnInit {
    constructor(protected VisitantesService: VisitantesService) { }

    visitantes: Visitante[] = [];

    visitante = {
        nombre: '',
        apellido: '',
        avatar: '',
        id_tipo_documento: '',
        numero_documento: '',
        id_tipo_visita: '',
        patente: '',
        telefono: '',
        observaciones: '',
    };

    actualizarListado() {
        this.VisitantesService.getAllVisitantes().then(response => {
            this.visitantes = response.data;
        });
    }

    ngOnInit(): void {
        this.actualizarListado();
    }
}
