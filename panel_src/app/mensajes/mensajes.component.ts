import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import * as moment from 'moment';

import { PublicacionesService } from '../publicaciones/publicacion.service';
import { Publicacion } from '../publicaciones/publicacion.model';

moment.locale('es');

@Component({
  selector: 'app-mensajes',
  templateUrl: './mensajes.component.html',
  styleUrls: ['./mensajes.component.css']
})
export class MensajesComponent implements OnInit {
  public publicacionesAgrupadas: any = [];
  cargando: boolean;
  constructor(public PublicacionesService: PublicacionesService, private modalService: NgbModal, private notificaciones: NotificationsService) { }

  ngOnInit() {
    this.cargarPublicaciones();
  }

  async cargarPublicaciones() {
    this.cargando=true;
    try {
      const response = await this.PublicacionesService.getMensajes();

      if (response.error) {
        throw 'error';
      }
      this.cargando=false;

      const publicacionesAgrupadas = response.data.reduce<any>((obj, publicacion) => {
        const fecha = this.getFormatDate(publicacion.fecha);

        if (!(fecha in obj)) {
          obj[fecha] = [];
        }

        obj[fecha].push(publicacion);

        return obj;
      }, {});

      this.publicacionesAgrupadas = Object.keys(publicacionesAgrupadas).map(key => ({ key, publicaciones: publicacionesAgrupadas[key] }));
    } catch (error) {
      this.notificaciones.error('Error', 'No se pueden cargar los mensajes.');
    }
  }

  getFormatDate(date) {
    const momentDate = moment(date);
    const momentToday = moment(Date.now());
    const momentYesterday = momentToday.clone().add(-1, 'day');

    if (momentToday.format('YYYYMMDD') === momentDate.format('YYYYMMDD')) {
      return 'Hoy';
    }

    if (momentYesterday.format('YYYYMMDD') === momentDate.format('YYYYMMDD')) {
      return 'Ayer';
    }

    if (momentToday.format('YYYY') !== momentDate.format('YYYY')) {
      return moment(date).format('DD [de] MMMM [del] YY');
    }

    return moment(date).format('DD [de] MMMM');
  }
}
