import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { TurnosService } from './turnos.service';
import { Turno } from './turno.model';
import { ActivatedRoute } from '@angular/router';
import { CalendarComponent } from "ap-angular2-fullcalendar";
import * as moment from 'moment';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { ConfirmationService } from '@jaspero/ng2-confirmations';

@Component({
  selector: 'app-turnos',
  templateUrl: './turnos.component.html',
  styleUrls: ['./turnos.component.scss']
})

export class TurnosComponent implements OnInit {
  @ViewChild('crearTurnoModal') crearTurnoModal: TemplateRef<NgbModalRef>;
  @ViewChild('actualizarTurnoModal') actualizarTurnoModal: TemplateRef<NgbModalRef>;
  @ViewChild(CalendarComponent) myCalendar: CalendarComponent;

  constructor(
    protected TurnosService: TurnosService,
    private route: ActivatedRoute,
    private modalService: NgbModal,
    private notificaciones: NotificationsService,
    private confirmacion: ConfirmationService
  ) { }

  private sub;
  private amenity: number;
  private modalRef: NgbModalRef;
  private defaultDate = new Date(2017, 1, 6);

  public turnos = [];
  public dias = [];
  public turno = {};

  async traerDias() {
    const response = await this.TurnosService.getAllDias();

    this.dias = response.data;
  }

  async actualizarListado() {
    const { data: turnos } = await this.TurnosService.getAllTurnos(this.amenity);

    this.myCalendar.fullCalendar('removeEvents');

    for (let turno of turnos) {
      this.myCalendar.fullCalendar('renderEvent', this.buildEvent(turno), true);
    }

    this.turnos = turnos;
  }

  buildEvent(turno: Turno) {
    const [hours, minutes] = turno.horaDesde.split(':');
    const start = moment(this.defaultDate).clone().add(+hours, 'hours').add(+minutes, 'minutes').add(turno.idDiaSemana - 1, 'days');
    const end = start.clone().add(turno.duracion, 'minutes');

    return {
      title: turno.costo ? `${turno.nombre} - $${turno.costo.toFixed(2)}` : turno.nombre,
      start,
      end,
      id: turno.id,
      turno
    };
  }

  abrirModalTurno(start?: moment.Moment, end?: moment.Moment) {
    this.turno = {
      desde: start && start.format('HH:mm'),
      hasta: end && end.format('HH:mm'),
      dia: (start && start.weekday() + 1) || 1
    };

    this.myCalendar.fullCalendar('unselect');
    this.modalRef = this.modalService.open(this.crearTurnoModal);
  }

  async eventChange({ start, end, id }: { id: number, start: moment.Moment, end: moment.Moment }, delta: moment.Duration, revertFunc: Function) {
    const turno: Turno = {
      duracion: end.diff(start) / 1000 / 60,
      horaDesde: start.format('HH:mm'),
      idDiaSemana: start.weekday() + 1
    };

    try {
      const response = await this.TurnosService.actualizarTurno(id, turno);

      await this.actualizarListado();
      this.notificaciones.success("Éxito", "Se modificó correctamente el turno");
    } catch (error) {
      revertFunc();
      this.notificaciones.error("Error", "No se pudo modificar el turno");
    }
  }

  async crearTurno(turnoObj) {
    const duracion = moment(turnoObj.hasta, 'HH:mm').diff(moment(turnoObj.desde, 'HH:mm')) / 1000 / 60;
    const turno: Turno = {
      nombre: turnoObj.nombre,
      costo: turnoObj.costo,
      duracion,
      horaDesde: turnoObj.desde,
      idDiaSemana: turnoObj.dia
    };

    try {
      const response = await this.TurnosService.crearTurno(this.amenity, turno);

      this.modalRef.close();
      this.actualizarListado();
      this.notificaciones.success("Éxito", "Se creó correctamente el turno");
    } catch (error) {
      this.notificaciones.error("Error", "No se pudo crear el turno");
    }
  }

  async actualizarTurno(turnoObj) {
    const duracion = moment(turnoObj.hasta, 'HH:mm').diff(moment(turnoObj.desde, 'HH:mm')) / 1000 / 60;
    const turno: Turno = {
      nombre: turnoObj.nombre,
      costo: turnoObj.costo,
      duracion,
      horaDesde: turnoObj.desde,
      idDiaSemana: turnoObj.dia
    };

    try {
      const response = await this.TurnosService.actualizarTurno(turnoObj.id, turno);

      this.modalRef.close();
      this.actualizarListado();
      this.notificaciones.success("Éxito", "Se pudo modificar correctamente el turno");
    } catch (error) {
      this.notificaciones.error("Error", "No se pudo modificar el turno");
    }
  }

  async borrarTurno(turnoObj) {
    try {
      const response = await this.TurnosService.deleteTurno(turnoObj.id);

      this.modalRef.close();
      this.actualizarListado();
      this.notificaciones.success("Éxito", "Se borró correctamente el turno");
    } catch (error) {
      this.notificaciones.error("Error", "No se pudo borrar el turno");
    }
  }

  openTurno({ turno }: { turno: Turno }) {
    const [hours, minutes] = turno.horaDesde.split(':');
    const start = moment(this.defaultDate).clone().add(+hours, 'hours').add(+minutes, 'minutes').add(turno.idDiaSemana - 1, 'days');
    const end = start.clone().add(turno.duracion, 'minutes');

    this.turno = {
      nombre: turno.nombre,
      costo: turno.costo,
      dia: turno.idDiaSemana,
      desde: start.format('HH:mm'),
      hasta: end.format('HH:mm'),
      id: turno.id
    }
    this.modalRef = this.modalService.open(this.actualizarTurnoModal);
  }

  ngOnInit(): void {
    this.sub = this.route.params.subscribe(params => {
      this.amenity = +params['amenity'];
    });

    this.traerDias();
  }

  confirmacionEliminar(turno: Turno) {
    this.confirmacion.create('Confirmación', '¿Está seguro qué desea borrar?', { showCloseButton: true, confirmText: "SI", declineText: "NO" })
      .subscribe((ans: any) => {
        if (ans.resolved) {
          this.borrarTurno(turno);
        }
      });
  }

  onCalendarInit() {
    if (!this.amenity) {
      return setTimeout(() => this.onCalendarInit(), 1000);
    }

    this.actualizarListado();
  }

  calendarOptions = {
    locale: 'es',
    defaultDate: this.defaultDate,
    eventOverlap: false,
    selectOverlap: false,
    header: false,
    allDaySlot: false,
    editable: true,
    views: {
      settimana: {
        type: 'agendaWeek',
        duration: {
          days: 7
        },
        startOf: 'week',
        title: 'Apertura',
        columnFormat: 'dddd',
        firstDay: 1
      }
    },
    defaultView: 'settimana',
    selectable: true,
    selectHelper: true,
    select: this.abrirModalTurno.bind(this),
    eventConstraint: {
      start: '00:00',
      end: '24:00',
    },
    selectConstraint: {
      start: '00:00',
      end: '24:00',
    },
    eventResize: this.eventChange.bind(this),
    eventDrop: this.eventChange.bind(this),
    eventClick: this.openTurno.bind(this)
  };
}
