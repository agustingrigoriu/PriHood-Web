import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { QRCodeModule } from 'angular2-qrcode';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';

import { EmpleadosComponent } from './empleados/empleados.component';
import { EmpleadosService } from './empleados/empleados.service';

import { ResidenciaComponent } from './residencias/residencias.component';
import { ResidenciasService } from './residencias/residencias.service';

import { BarriosComponent } from './barrios/barrios.component';
import { BarriosService } from './barrios/barrios.service';

import { VisitasComponent } from './visitas/visitas.component';
import { VisitasService } from './visitas/visitas.service';

import { ApiRequestService } from '../services/api.request.service';
import { LoginService } from '../services/login.service';
import { Ng2SearchPipeModule } from '../modules/filter/ng2-filter.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    Ng2SearchPipeModule,
    QRCodeModule,
    NgbModule.forRoot(),
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
        path: 'empleados',
        component: EmpleadosComponent
      },
      {
        path: 'residencias',
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
    EmpleadosComponent,
    ResidenciaComponent,
    BarriosComponent,
    VisitasComponent
  ],
  providers: [
    ApiRequestService,
    LoginService,
    EmpleadosService,
    ResidenciasService,
    BarriosService,
    VisitasService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
