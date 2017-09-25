import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PublicacionesService } from '../publicaciones/publicacion.service';
import { Publicacion } from '../publicaciones/publicacion.model';
import * as moment from 'moment';

moment.locale('es');

@Component({
  selector: 'app-publicacion',
  templateUrl: './publicacion.component.html',
  styleUrls: ['./publicacion.component.css']
})
export class PublicacionComponent implements OnInit {

  private sub;
  private id_publicacion: number;
  public publicacion: Publicacion;

  constructor(public PublicacionesService: PublicacionesService, private route: ActivatedRoute) {
    this.publicacion = {};
  }

  ngOnInit() {
    this.sub = this.route.params.subscribe(params => {
      this.id_publicacion = +params['publicacion'];
      this.cargarPublicacion();
    });
  }

  async cargarPublicacion() {
    try {
      const response = await this.PublicacionesService.getPublicacion(this.id_publicacion);

      if (response.error) {
        throw 'error';
      }

      this.publicacion = response.data;
    } catch (error) {
      alert('No se pueden cargar las publicaciones.')
    }
  }
}
