import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';

import { UsuariosComponent } from './usuarios/usuarios.component';
import { UsuarioComponent } from './usuarios/usuario.component';
import { UsuariosService } from './usuarios/usuarios.service';

import {ResidenciaComponent } from './residencias/residencias.component';
import {ResidenciasService } from './residencias/residencias.service';

import { ApiRequestService } from '../services/api.request.service';

@NgModule({
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    RouterModule.forRoot([
      {
        path: 'home',
        component: HomeComponent
      },
      {
        path: 'usuarios',
        component: UsuariosComponent
      },
      {
        path: 'usuarios/:id',
        component: UsuarioComponent
      },
      {
        path: 'residencia',
        component: ResidenciaComponent
      }
    ])
  ],
  declarations: [
    AppComponent,
    HomeComponent,
    UsuariosComponent,
    UsuarioComponent,
    ResidenciaComponent
  ],
  providers: [
    ApiRequestService,
    UsuariosService,
    ResidenciasService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
