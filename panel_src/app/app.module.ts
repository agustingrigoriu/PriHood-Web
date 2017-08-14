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

// import { BarriosComponent } from './barriospato/barrios.component';
// import { BarriosService } from './barriospato/barrios.service';
import {BarriosComponent} from './barrios/barrios.component';
import {BarriosService} from './barrios/barrios.service';

import { ApiRequestService } from '../services/api.request.service';
import { LoginService } from '../services/login.service';

@NgModule({
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    RouterModule.forRoot([
      {
        path: 'barrios',
        component: BarriosComponent
      },
      {
        path: '',
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
    ResidenciaComponent,
    BarriosComponent
  ],
  providers: [
    ApiRequestService,
    LoginService,
    UsuariosService,
    ResidenciasService,
    BarriosService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
