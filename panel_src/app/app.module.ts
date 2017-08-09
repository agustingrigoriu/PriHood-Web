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

import { BarriosComponent } from './barriospato/barrios.component';
import { BarriosService } from './barriospato/barrios.service';

import { ApiRequestService } from '../services/api.request.service';

@NgModule({
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    RouterModule.forRoot([
      {
        path: 'barriospato',
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
      }
    ])
  ],
  declarations: [
    AppComponent,
    HomeComponent,
    UsuariosComponent,
    UsuarioComponent,
    BarriosComponent
  ],
  providers: [
    ApiRequestService,
    UsuariosService,
    BarriosService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
