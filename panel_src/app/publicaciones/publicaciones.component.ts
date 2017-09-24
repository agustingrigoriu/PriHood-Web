import { Component, OnInit } from '@angular/core';
import { PublicacionesService } from './publicacion.service';
import * as moment from 'moment';

moment.locale('es');

@Component({
  selector: 'app-publicaciones',
  templateUrl: './publicaciones.component.html',
  styleUrls: ['./publicaciones.component.css']
})
export class PublicacionesComponent implements OnInit {

  public publicacionesAgrupadas: any = [];

  constructor(public PublicacionesService: PublicacionesService) { }

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
        const fecha = moment(publicacion.fecha).format('DD MMM YY');

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

}
