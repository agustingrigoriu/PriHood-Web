import { Component, OnInit } from '@angular/core';
import { VisitasService } from './visitas.service';
import { Visita } from './visita.model';

@Component({
  selector: 'app-visitas',
  templateUrl: './visitas.component.html',
  styleUrls: ['./visitas.component.css']
})

export class VisitasComponent implements OnInit {
  constructor(protected BarriosService: VisitasService) { }

visitas: Visita[] = [];
filtro:string;
  
  actualizarListado() {
    this.BarriosService.getAllVisitas().then(response => {
      this.visitas = response.data;
    });
  }

  ngOnInit(): void {
    this.actualizarListado();
  }
}
