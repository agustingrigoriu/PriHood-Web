import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgForm } from "@angular/forms/forms";
import { NotificationsService } from 'angular2-notifications';
import * as moment from 'moment';

import { PublicacionesService } from '../publicaciones/publicacion.service';
import { Publicacion } from '../publicaciones/publicacion.model';
import { Comentario } from '../publicaciones/comentario.model';

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
  public comentarios: Comentario[];
  public comentario: any;

  constructor(public PublicacionesService: PublicacionesService, private route: ActivatedRoute, private notificaciones: NotificationsService, private router: Router) {
    this.publicacion = {};
    this.comentarios = [];
    this.comentario = {};
  }

  ngOnInit() {
    this.sub = this.route.params.subscribe(params => {
      this.id_publicacion = +params['publicacion'];
      this.cargarPublicacion();
      this.cargarComentarios();
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
      this.notificaciones.error("Error", "No se pueden cargar las publicaciones");
    }
  }

  async cargarComentarios() {
    try {
      const response = await this.PublicacionesService.getComentarios(this.id_publicacion);

      if (response.error) {
        throw 'error';
      }

      this.comentarios = response.data;
    } catch (error) {
      this.notificaciones.error("Error", "No se pueden cargar los comentarios");
    }
  }

  async onSubmit(comentario: any, form: NgForm) {
    try {
      const response = await this.PublicacionesService.comentar(this.id_publicacion, comentario);

      if (response.error) {
        throw 'error';
      }

      await this.cargarComentarios();

      this.notificaciones.success("Éxito", "Se guardó correctamente el comentario");

      form.reset();
    } catch (error) {
      this.notificaciones.error("Error", "No se pudo guardar el comentario");
    }
  }

  async borrarPublicacion() {
    if (confirm('¿Seguro de borrar la publicación?')) {
      try {
        const response = await this.PublicacionesService.borrarPublicacion(this.id_publicacion);

        if(response.error) {
          throw 'error';
        }

        this.notificaciones.success("Éxito", "Se borró correctamente la publicación");

        this.router.navigate(['publicaciones']);
      } catch (error) {
        this.notificaciones.error("Error", "No se pudo borrar la publicación");
      }
    }
  }

  getFechaRelativa(fecha) {
    return moment(fecha).fromNow();
  }
}
