import { Component, OnInit } from '@angular/core';
import { VisitasService } from './visitas.service';
import { Visita } from './visita.model';

@Component({
    selector: 'app-visitas',
    templateUrl: '/visitas.component.html',
    styleUrls: ['/visitas.component.css']
})

export class VisitasComponent implements OnInit {
    constructor(protected VisitasService: VisitasService) { }

    visitas: Visita[] = [];
    fecha_actual = new Date();

    actualizarListado() {
        this.VisitasService.getAllVisitas().then(response => {
            this.visitas = response.error? [] : response.data;
        });
    }

    ngOnInit(): void {
        this.actualizarListado();
    }
}
