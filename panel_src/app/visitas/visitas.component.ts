import { Component, OnInit } from '@angular/core';
import { VisitasService } from './visitas.service';
import { Visita } from './visita.model';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';

@Component({
    selector: 'app-visitas',
    templateUrl: '/visitas.component.html',
    styleUrls: ['/visitas.component.css']
})

export class VisitasComponent implements OnInit {
    constructor(protected VisitasService: VisitasService, private modalService: NgbModal) { }

    visitas: Visita[] = [];
    fecha_actual = new Date();
    mensaje: string;
    headerClass = ""

    actualizarListado() {
        this.VisitasService.getAllVisitas().then(response => {
            this.visitas = response.error ? [] : response.data;
        });
    }

    marcarIngreso(visita: Visita) {
        this.VisitasService.marcarIngreso(visita.id).then(response => {
            if (response.error) {
                alert('No se pudo marcar la visita como ingresada.');
                this.mensaje = "No se pudo marcar la visita como ingresada";
                this.headerClass = "alert-danger"
            }
            this.mensaje = "Notificación de ingreso enviada";
            this.headerClass = "alert-success";
            return this.actualizarListado();
        });
    }

    marcarEgreso(visita: Visita) {
        this.VisitasService.marcarEgreso(visita.id).then(response => {
            if (response.error) {
                alert('No se pudo marcar la visita como egresada.');
                this.mensaje = "No se pudo marcar la visita como egresada";
            }
            this.mensaje = "Se marcó el egreso correctamente";
            return this.actualizarListado();
        });
    }

    ngOnInit(): void {
        this.actualizarListado();
    }

    open(marcarestado) {
        this.modalService.open(marcarestado, { windowClass: 'in' });
    }
}
