import { Component, OnInit } from '@angular/core';
import { ExpensasService } from './expensas.service';
import { NgForm } from "@angular/forms/forms";
import { NotificationsService } from 'angular2-notifications';
import { ConfirmationService } from '@jaspero/ng2-confirmations';
import { Residencia } from '../residencias/residencia.model';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';

@Component({
    selector: 'app-expensas',
    templateUrl: './expensas.component.html',
    styleUrls: ['./expensas.component.css']
})

export class ExpensaComponent implements OnInit {
    constructor(protected ExpensasService: ExpensasService, private notificaciones: NotificationsService, private confirmacion: ConfirmationService) { }

    residencias: Residencia[] = [];
    residencia: Residencia = {
        nombre: '',
        ubicacion: ''
    };
    residenciaSeleccionada: Residencia;

    onSelectedRes(residencia: Residencia) {
        this.residenciaSeleccionada = { ...residencia };
    }

    actualizar() {
        this.ExpensasService.getAllResidencias().then(response => {
            if (response.error) {
                alert('No se pudo cargar las residencias.');
            } else {
                this.residencias = response.data;
            }
        });
    }

    imprimirListado() {
        window.print();
    }

    ngOnInit(): void {
        this.actualizar();
    }
}
