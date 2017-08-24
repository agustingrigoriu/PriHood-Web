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

    marcarIngreso(visita: Visita) {
        this.VisitasService.marcarIngreso(visita.id).then(response => {
            if(response.error) {
                alert('No se pudo marcar la visita como ingresada.');
            }

            return this.actualizarListado();
        });
    }

    marcarEgreso(visita: Visita) {
        this.VisitasService.marcarEgreso(visita.id).then(response => {
            if(response.error) {
                alert('No se pudo marcar la visita como egresada.');
            }

            return this.actualizarListado();
        });
    }

    ngOnInit(): void {
        this.actualizarListado();
    }
}
