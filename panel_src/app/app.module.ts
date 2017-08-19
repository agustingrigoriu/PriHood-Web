import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { QRCodeModule } from 'angular2-qrcode';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';

import { UsuariosComponent } from './usuarios/usuarios.component';
import { UsuarioComponent } from './usuarios/usuario.component';
import { UsuariosService } from './usuarios/usuarios.service';

import {ResidenciaComponent } from './residencias/residencias.component';
import {ResidenciasService } from './residencias/residencias.service';

import {BarriosComponent} from './barrios/barrios.component';
import {BarriosService} from './barrios/barrios.service';

import {VisitasComponent} from './visitas/visitas.component';
import {VisitasService} from './visitas/visitas.service';

import { ApiRequestService } from '../services/api.request.service';
import { LoginService } from '../services/login.service';

@NgModule({
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    QRCodeModule,
    RouterModule.forRoot([
      {
        path: 'barrios',
        component: BarriosComponent
      },
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
        path: 'residencias/:barrio',
        component: ResidenciaComponent
      },
      {
        path: 'visitas',
        component: VisitasComponent
      },
      {
        path: '**',
        redirectTo: 'home'
      }
    ])
  ],
  declarations: [
    AppComponent,
    HomeComponent,
    UsuariosComponent,
    UsuarioComponent,
    ResidenciaComponent,
    BarriosComponent,
    VisitasComponent
  ],
  providers: [
    ApiRequestService,
    LoginService,
    UsuariosService,
    ResidenciasService,
    BarriosService,
    VisitasService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
