import { Component, OnInit } from "@angular/core";
import { LoginService } from "../../services/login.service";
import { Usuario } from "../usuarios/usuario.model";
import { RootDashboard, AdminDashboard } from "./home.model";
import { DashboardService } from "./home.service";

@Component({
  selector: "app-home",
  templateUrl: "./home.component.html",
  styleUrls: ["./home.component.css"]
})
export class HomeComponent implements OnInit {
  constructor(
    protected LoginService: LoginService,
    protected DashboardService: DashboardService
  ) {}
  title = "Home";
  mensaje: string;
  headerClass: string = " ";

  usuario: Usuario = {
    email: "",
    idPerfil: -1,
    nombre: "",
    apellido: "",
    perfil: "",
    barrio: ""
  };

  rootDashboard: RootDashboard = {
    cantidad_barrios: 0
  };

  adminDashboard: AdminDashboard = {
    cantidad_residencias: 0,
    cantidad_residentes: 0,
    cantidad_amenities: 0,
    latitud: -31.335335,
    longitud: -64.303113,
    visitasFrecuentesDataBar: [],
    visitasActualDataBar: [],
    amenitiesDataPie: [],
    recaudacionReservasLine: []
  };

  //bar Chart Visitas (Frecuentes y Actuales)
  public barChartOptions: any = {
    scaleShowVerticalLines: false,
    responsive: true
  };
  public barChartType: string = "bar";
  public barChartLegend: boolean = true;

  public barChartLabels: string[] = [];
  public barChartData: any[] = [
    { data: [2], label: "Visitas Frecuentes" },
    { data: [2], label: "Visitas Actuales" }
  ];

  // Gráfico de torta para tipos amenities más reservados
  public pieChartLabels: string[] = [];
  public pieChartData: number[] = [];
  public pieChartType: string = "pie";
  public pieChartColors: any[] = [{ backgroundColor: ["#b8436d", "#00d9f9", "#a4c73c", "#a4add3", '#511730', '#8e443d', '#e0d68a'] }];

  // Line chart para recaudaciones de amenities por mes
  public lineChartData:Array<any> = [
    {data: [], label: 'Recaudaciones'},
  ];
  public lineChartLabels:Array<any> = [];
  public lineChartOptions:any = {
    responsive: true,
    maintainAspectRatio: true
  };
  public lineChartColors:Array<any> = [
    { // grey
      backgroundColor: 'rgba(148,159,177,0.2)',
      borderColor: 'rgba(148,159,177,1)',
      pointBackgroundColor: 'rgba(148,159,177,1)',
      pointBorderColor: '#fff',
      pointHoverBackgroundColor: '#fff',
      pointHoverBorderColor: 'rgba(148,159,177,0.8)'
    },
    { // dark grey
      backgroundColor: 'rgba(77,83,96,0.2)',
      borderColor: 'rgba(77,83,96,1)',
      pointBackgroundColor: 'rgba(77,83,96,1)',
      pointBorderColor: '#fff',
      pointHoverBackgroundColor: '#fff',
      pointHoverBorderColor: 'rgba(77,83,96,1)'
    },
    { // grey
      backgroundColor: 'rgba(148,159,177,0.2)',
      borderColor: 'rgba(148,159,177,1)',
      pointBackgroundColor: 'rgba(148,159,177,1)',
      pointBorderColor: '#fff',
      pointHoverBackgroundColor: '#fff',
      pointHoverBorderColor: 'rgba(148,159,177,0.8)'
    }
  ];
  public lineChartLegend:boolean = true;
  public lineChartType:string = 'line';

  drawVisitsGraphBar() {

    let clone_data = JSON.parse(JSON.stringify(this.barChartData));
    this.barChartLabels.length = 0;
    var labels = this.adminDashboard.visitasFrecuentesDataBar.map(a => a.label);
    for (let i = labels.length - 1; i >= 0; i--) {
      this.barChartLabels.push(labels[i]);
    }
    let clone_labels = JSON.parse(JSON.stringify(this.barChartLabels));
    clone_labels = this.adminDashboard.visitasFrecuentesDataBar.map(a => a.label)
    clone_data[0].data = this.adminDashboard.visitasFrecuentesDataBar.map(a => a.count);
    clone_data[1].data = this.adminDashboard.visitasActualDataBar.map(a => a.count);
    this.barChartData = clone_data;
    this.barChartLabels = clone_labels;
    console.log(this.barChartLabels);

  }

  drawAmenitiesPie() {
    let clone_data = JSON.parse(JSON.stringify(this.pieChartData));
    this.pieChartLabels.length = 0;
    var labels = this.adminDashboard.amenitiesDataPie.map(a => a.label);
    for (let i = labels.length - 1; i >= 0; i--) {
      this.pieChartLabels.push(labels[i]);
    }
    clone_data = this.adminDashboard.amenitiesDataPie.map(a => a.count);
    this.pieChartData = clone_data;

  }

  drawAmenitiesLine() {
    let clone_data = JSON.parse(JSON.stringify(this.lineChartData));
    this.lineChartLabels.length = 0;
    var labels = this.adminDashboard.recaudacionReservasLine.map(a => a.label);
    for (let i = labels.length - 1; i >= 0; i--) {
      this.lineChartLabels.push(labels[i]);
    }
    clone_data[0] = this.adminDashboard.recaudacionReservasLine.map(a => a.sum);
    this.lineChartData = clone_data;
  }

  // events
  public chartClicked(e: any): void {
    console.log(e);
  }

  public chartHovered(e: any): void {
    console.log(e);
  }

  getUsuario() {
    this.LoginService.getUserLogin().then(response => {
      if (response.error) {
        this.mensaje = "No se pudo traer el usuario.";
        this.headerClass = "alert-danger";
      } else {
        this.usuario = response.data;

        if (this.usuario.idPerfil === 1) {
          this.getRootDashboard();
        }

        if (this.usuario.idPerfil === 2) {
          this.getAdminDashboard();
        }
      }
    });
  }

  getRootDashboard() {
    this.DashboardService.getRootDashboard().then(response => {
      if (response.error) {
        this.mensaje = "No se pudo traer datos de dashboard.";
        this.headerClass = "alert-danger";
      } else {
        this.rootDashboard = response.data;
      }
    });
  }

  getAdminDashboard() {
    this.DashboardService.getAdminDashboard().then(response => {
      if (response.error) {
        this.mensaje = "No se pudo traer datos de dashboard.";
        this.headerClass = "alert-danger";
      } else {
        this.adminDashboard = response.data;
        this.drawAmenitiesPie();
        this.drawVisitsGraphBar();
        this.drawAmenitiesLine();
      }
    });
  }

  ngOnInit() {
    this.getUsuario();
  }
}
