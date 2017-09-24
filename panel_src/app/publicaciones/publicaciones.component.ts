import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { PublicacionesService } from './publicacion.service';
import { Publicacion } from './publicacion.model';
import * as moment from 'moment';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';

moment.locale('es');

@Component({
  selector: 'app-publicaciones',
  templateUrl: './publicaciones.component.html',
  styleUrls: ['./publicaciones.component.css']
})
export class PublicacionesComponent implements OnInit {
  @ViewChild('crearPublicacion') crearPublicacionModal: TemplateRef<NgbModalRef>;

  public publicacionesAgrupadas: any = [];
  public publicacion: Publicacion;
  private modalRef: NgbModalRef;

  constructor(public PublicacionesService: PublicacionesService, private modalService: NgbModal) { }

  ngOnInit() {
    this.cargarPublicaciones();
  }

  async cargarPublicaciones() {
    try {
      const response = await this.PublicacionesService.getPublicaciones();

      if (response.error) {
        throw 'error';
      }

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
      alert('No se pueden cargar las publicaciones.')
    }
  }

  abrirModal() {
    this.publicacion = {
      titulo: '',
      texto: ''
    };

    this.modalRef = this.modalService.open(this.crearPublicacionModal);
  }

  async registrarPublicacion(publicacion) {
    const response = await this.PublicacionesService.crearPublicacion(publicacion);

    this.modalRef.close();
    this.cargarPublicaciones();
  }

  getFormatDate(date) {
    const momentDate = moment(date);
    const momentToday = moment(Date.now());

    if (momentToday.format('YYYYMMDD') === momentDate.format('YYYYMMDD')) {
      return 'Hoy';
    }

    if (momentToday.format('YYYY') !== momentDate.format('YYYY')) {
      return moment(date).format('DD MM YY');
    }

    return moment(date).format('DD MM');
  }

}
