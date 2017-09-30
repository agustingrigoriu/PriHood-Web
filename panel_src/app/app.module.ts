import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { QRCodeModule } from 'angular2-qrcode';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { DashboardService } from './home/home.service';

import { EmpleadosComponent } from './empleados/empleados.component';
import { EmpleadosService } from './empleados/empleados.service';

import { ResidenciaComponent } from './residencias/residencias.component';
import { ResidenciasService } from './residencias/residencias.service';

import { BarriosComponent } from './barrios/barrios.component';
import { BarriosService } from './barrios/barrios.service';

import { VisitasComponent } from './visitas/visitas.component';
import { VisitasService } from './visitas/visitas.service';

import { TurnosComponent } from './turnos/turnos.component';
import { TurnosService } from './turnos/turnos.service';

import { AmenitiesComponent } from './amenities/amenities.component';
import { AmenitiesService } from './amenities/amenities.service';

import { PublicacionesComponent } from './publicaciones/publicaciones.component';
import { PublicacionComponent } from './publicacion/publicacion.component';
import { PublicacionesService } from './publicaciones/publicacion.service';

import { ExpensasComponent } from './expensas/expensas.component';
import { ExpensasService } from './expensas/expensas.service';

import { ExpensaComponent } from './expensa/expensa.component'
import { ExpensaService } from './expensa/expensa.service';

import { ApiRequestService } from '../services/api.request.service';
import { LoginService } from '../services/login.service';
import { Ng2SearchPipeModule } from '../modules/filter/ng2-filter.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CalendarModule } from "ap-angular2-fullcalendar";
import { AgmCoreModule } from '@agm/core';
import { UsuarioComponent } from './usuario/usuario.component';

import { SimpleNotificationsModule } from 'angular2-notifications';
import { JasperoConfirmationsModule } from '@jaspero/ng2-confirmations';
import { DpDatePickerModule } from 'ng2-date-picker';

@NgModule({
  imports: [
    JasperoConfirmationsModule,
    BrowserModule,
    FormsModule,
    HttpClientModule,
    Ng2SearchPipeModule,
    QRCodeModule,
    CalendarModule,
    BrowserAnimationsModule,
    SimpleNotificationsModule.forRoot(),
    NgbModule.forRoot(),
    DpDatePickerModule,
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
        path: 'amenities',
        component: AmenitiesComponent
      },
      {
        path: 'usuario',
        component: UsuarioComponent
      },
      {
        path: 'turnos/:amenity',
        component: TurnosComponent
      },
      {
        path: 'publicaciones',
        component: PublicacionesComponent
      },
      {
        path: 'publicaciones/:publicacion',
        component: PublicacionComponent
      },
      {
        path: 'expensas',
        component: ExpensasComponent
      },
      {
        path: 'expensas/:expensa',
        component: ExpensaComponent
      },
      {
        path: '**',
        redirectTo: 'home'
      }
    ]),
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyAAMmwIYbNte2GOoB1vYoAK5Sz5oPpZ6JA'
    })
  ],
  declarations: [
    AppComponent,
    HomeComponent,
    EmpleadosComponent,
    ResidenciaComponent,
    BarriosComponent,
    VisitasComponent,
    AmenitiesComponent,
    TurnosComponent,
    UsuarioComponent,
    PublicacionesComponent,
    PublicacionComponent,
    ExpensasComponent,
    ExpensaComponent
  ],
  providers: [
    ApiRequestService,
    LoginService,
    EmpleadosService,
    ResidenciasService,
    BarriosService,
    VisitasService,
    AmenitiesService,
    TurnosService,
    DashboardService,
    PublicacionesService,
    ExpensasService,
    ExpensaService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
