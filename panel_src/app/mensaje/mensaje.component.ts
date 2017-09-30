import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgForm } from "@angular/forms/forms";
import { NotificationsService } from 'angular2-notifications';
import { ConfirmationService } from '@jaspero/ng2-confirmations';
import * as moment from 'moment';

import { PublicacionesService } from '../publicaciones/publicacion.service';
import { Publicacion } from '../publicaciones/publicacion.model';
import { Comentario } from '../publicaciones/comentario.model';

moment.locale('es');

@Component({
  selector: 'app-mensaje',
  templateUrl: './mensaje.component.html',
  styleUrls: ['./mensaje.component.css']
})
export class MensajeComponent implements OnInit {
  private sub;
  private id_publicacion: number;
  public publicacion: Publicacion;
  public comentarios: Comentario[];
  public comentario: any;

  constructor(public PublicacionesService: PublicacionesService, private route: ActivatedRoute, private notificaciones: NotificationsService, private router: Router, private confirmacion: ConfirmationService) {
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
      const response = await this.PublicacionesService.getMensaje(this.id_publicacion);

      if (response.error) {
        throw 'error';
      }

      this.publicacion = response.data;
    } catch (error) {
      this.notificaciones.error("Error", "No se pueden cargar el mensaje");
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
    try {
      const response = await this.PublicacionesService.borrarPublicacion(this.id_publicacion);

      if (response.error) {
        throw 'error';
      }

      this.notificaciones.success("Éxito", "Se borró correctamente la publicación");

      this.router.navigate(['publicaciones']);
    } catch (error) {
      this.notificaciones.error("Error", "No se pudo borrar la publicación");
    }
  }

  confirmacionEliminar() {
    this.confirmacion.create('Confirmación', '¿Está seguro qué desea borrar?', { showCloseButton: true, confirmText: "SI", declineText: "NO" })
      .subscribe((ans: any) => {
        if (ans.resolved) {
          this.borrarPublicacion();
        }
      });
  }

  getFechaRelativa(fecha) {
    return moment(fecha).fromNow();
  }
}
