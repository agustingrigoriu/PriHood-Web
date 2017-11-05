import { Component, OnInit } from '@angular/core';
import { ExpensasService } from './expensas.service';
import { NgForm } from "@angular/forms/forms";
import { NotificationsService } from 'angular2-notifications';
import { ConfirmationService } from '@jaspero/ng2-confirmations';
import { Residencia } from '../residencias/residencia.model';
import { Expensa } from './expensa.model';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { IDatePickerConfig, IMonthCalendarConfig } from 'ng2-date-picker';

@Component({
    selector: 'app-expensas',
    templateUrl: './expensas.component.html',
    styleUrls: ['./expensas.component.css']
})

export class ExpensasComponent implements OnInit {
    constructor(protected ExpensasService: ExpensasService, private notificaciones: NotificationsService, private confirmacion: ConfirmationService) {
        this.residencias = [];
        this.expensa = {};
    }

    residencias: Residencia[];
    residenciaSeleccionada: Residencia;
    expensa: Expensa;
    file: File;
    p: number = 1;
    datePickerConfig: IDatePickerConfig = {
        format: 'DD/MM/YYYY',
        locale: 'es',
    };
    monthPickerConfig: IMonthCalendarConfig = {
        format: 'MMMM [del] YYYY',
        locale: 'es',
    };

    onSelectedRes(residencia: Residencia) {
        this.residenciaSeleccionada = { ...residencia };
        this.expensa.id_residencia = residencia.id;
    }

    actualizar() {
        this.ExpensasService.getAllResidencias().then(response => {
            if (response.error) {
                this.notificaciones.error('Error', 'No se pudo cargar las residencias.');
            } else {
                this.residencias = response.data;
            }
        });
    }

    ngOnInit(): void {
        this.actualizar();
    }

    onChangeFile(event: EventTarget) {
        let eventObj: MSInputMethodContext = <MSInputMethodContext>event;
        let target: HTMLInputElement = <HTMLInputElement>eventObj.target;
        let files: FileList = target.files;

        this.file = files.item(0);
    }

    async agregarExpensa(expensa: any, file: File, form: NgForm) {
        try {
            // @FIXME: HARDCODE!!!
            expensa.fecha_expensa = expensa.fecha_expensa.toISOString();
            expensa.fecha_vencimiento = expensa.fecha_vencimiento.toISOString();
            // FIN HARDCODE

            const response = await this.ExpensasService.cargarExpensa(expensa, file);

            if(response.error) throw 'error';


            this.notificaciones.success('Éxito', 'Se cargó correctamente la expensa.');
            console.log(response);
            form.reset();
        } catch (error) {
            this.notificaciones.error('Error', 'No se pudo cargar la expensa.');
        }
    }
}
