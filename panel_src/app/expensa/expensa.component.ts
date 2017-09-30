import { Component, OnInit } from '@angular/core';
import { ExpensaService } from './expensa.service';
import { NgForm } from "@angular/forms/forms";
import { NotificationsService } from 'angular2-notifications';
import { ConfirmationService } from '@jaspero/ng2-confirmations';
import { Residencia } from '../residencias/residencia.model';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { ActivatedRoute, Router } from '@angular/router';
import { Expensa } from '../expensas/expensa.model';
import * as moment from 'moment';

@Component({
    selector: 'app-expensa',
    templateUrl: './expensa.component.html',
    styleUrls: ['./expensa.component.css']
})

export class ExpensaComponent implements OnInit {
    constructor(protected ExpensaService: ExpensaService, private notificaciones: NotificationsService, private confirmacion: ConfirmationService, private route: ActivatedRoute) { }
    private sub;
    public id_residencia: number;
    public aliasResidencia: string;

    residencia: Residencia = {
        nombre: '',
        ubicacion: ''
    };
    expensas: Expensa[] = [];

    async cargarResidencia() {
        try {
            const response = await this.ExpensaService.getResidencia(this.id_residencia);

            if (response.error) {
                throw 'error';
            }

            this.residencia = response.data;
        } catch (error) {
            this.notificaciones.error("Error", "No se puede cargar la residencia");
        }
    }

    ngOnInit(): void {
        this.sub = this.route.params.subscribe(params => {
            this.id_residencia = +params['expensa'];
            this.cargarResidencia();
            this.actualizarListado();
        });
    }

    actualizarListado() {
        this.ExpensaService.getExpensas(this.id_residencia).then(response => {
            if (response.error) {
                alert('No se pudo cargar las expensas.');
            } else {
                this.expensas = response.data;
            }
        });
    }

    getFormatoMes(fecha: Date) {
        return moment(fecha).format('MMMM / YYYY').toUpperCase();
    }
}
