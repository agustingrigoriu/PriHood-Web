import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { AlertasService } from './alertas.service';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';

import { Alerta } from './alerta.model';

@Component({
    selector: 'app-alertas',
    templateUrl: './alertas.component.html',
    styleUrls: ['./alertas.component.css']
})

export class AlertasComponent implements OnInit, OnDestroy {
    @ViewChild('modalAlertas') modalAlertas;

    alertas: Alerta[];
    alerta: Alerta;
    index: number;

    sub;
    tiempo: number = 10 * 1000; // 10 segundos
    modal: NgbModalRef;

    constructor(protected AlertasService: AlertasService, private notificaciones: NotificationsService, private NgbModal: NgbModal) {
        this.alertas = [];
        this.index = 0;
        this.alerta = {
            descripcion: '',
            fecha: new Date(),
            residente: '',
            tipo_alerta: ''
        };
    }

    anterior() {
        if (this.index - 1 >= 0) {
            this.index--;
        } else {
            this.index = this.alertas.length - 1;
        }

        this.alerta = this.alertas[this.index];
    }

    proxima() {
        if (this.index + 1 < this.alertas.length) {
            this.index++;
        } else {
            this.index = 0;
        }

        this.alerta = this.alertas[this.index];
    }

    async visto(alerta: Alerta) {
        try {
            const index = this.alertas.indexOf(alerta);
            this.alertas.splice(index, 1);
            this.proxima();

            if (this.alertas.length === 0) {
                this.modal.close();
                this.modal = null;
            }

            const response = await this.AlertasService.marcarVisto(alerta);
            if (response.error) throw 'error';

            this.notificaciones.success('Éxitos', 'Se marcó como vista la alerta.');
        } catch (error) {
            this.notificaciones.error('Error', 'No se pudo marcar como visto.');
        }
    }

    playSound() {
        const alerta = new Audio('/panel/assets/sound/alerta.mp3');
        alerta.play();
    }

    mostrarModal() {
        if (!this.modal && this.alertas.length > 0) {
            this.modal = this.NgbModal.open(this.modalAlertas, { keyboard: false, backdrop: 'static' });
            this.index = 0;
            this.alerta = this.alertas[this.index];

            this.playSound();
        }
    }

    async actualizar() {
        try {
            const response = await this.AlertasService.getAllAlertas();

            if (response.error) return;

            this.alertas = response.data;

            this.mostrarModal();
        } catch (error) {
            this.notificaciones.error('Error', 'No se puedieron cargar las alertas.');
            console.error(error);
        }

        this.sub = setTimeout(() => this.actualizar(), this.tiempo);
    }

    ngOnInit(): void {
        this.actualizar();
    }

    ngOnDestroy(): void {
        clearTimeout(this.sub);
    }
}
