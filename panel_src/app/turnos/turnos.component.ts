import { Component, OnInit } from '@angular/core';
import { TurnosService } from './turnos.service';
import { Turno } from './turno.model';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-turnos',
  templateUrl: './turnos.component.html',
  styleUrls: ['./turnos.component.css']
})

export class TurnosComponent implements OnInit {
  constructor(
    protected TurnosService: TurnosService,
    private route: ActivatedRoute
  ) { }

  private sub;
  private amenity: number;
  public turnos: Turno[] = [];

  actualizarListado() {
    this.TurnosService.getAllTurnos(this.amenity).then(response => {
      this.turnos = response.data;
    });
  }

  ngOnInit(): void {
    this.sub = this.route.queryParams.subscribe(params => {
      this.amenity = +params['amenity'];

      this.actualizarListado();
    });
  }


  calendarOptions:Object = {
    height: 'parent',
    fixedWeekCount : false,
    defaultDate: '2016-09-12',
    editable: true,
    eventLimit: true, // allow "more" link when too many events
    events: [
      {
        title: 'All Day Event',
        start: '2016-09-01'
      },
      {
        title: 'Long Event',
        start: '2016-09-07',
        end: '2016-09-10'
      },
      {
        id: 999,
        title: 'Repeating Event',
        start: '2016-09-09T16:00:00'
      },
      {
        id: 999,
        title: 'Repeating Event',
        start: '2016-09-16T16:00:00'
      },
      {
        title: 'Conference',
        start: '2016-09-11',
        end: '2016-09-13'
      },
      {
        title: 'Meeting',
        start: '2016-09-12T10:30:00',
        end: '2016-09-12T12:30:00'
      },
      {
        title: 'Lunch',
        start: '2016-09-12T12:00:00'
      },
      {
        title: 'Meeting',
        start: '2016-09-12T14:30:00'
      },
      {
        title: 'Happy Hour',
        start: '2016-09-12T17:30:00'
      },
      {
        title: 'Dinner',
        start: '2016-09-12T20:00:00'
      },
      {
        title: 'Birthday Party',
        start: '2016-09-13T07:00:00'
      },
      {
        title: 'Click for Google',
        url: 'http://google.com/',
        start: '2016-09-28'
      }
    ]
  };

  onCalendarInit() {
    console.log('Calendar initialized');
  }
}
