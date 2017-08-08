import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { UsuariosService } from './usuarios.service';
import { Usuario } from './usuario.model';

@Component({
  selector: 'app-usuario',
  templateUrl: './usuario.component.html',
})

export class UsuarioComponent implements OnInit, OnDestroy {
  constructor(
    protected UsuariosService: UsuariosService,
    private route: ActivatedRoute
  ) { }

  usuario: Usuario;
  private sub: any;

  ngOnInit() {
    this.sub = this.route.params.subscribe(params => {
      this.UsuariosService.getUsuario(+params['id']).then(response => {
        this.usuario = response.data;
      });
    });
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }
}
