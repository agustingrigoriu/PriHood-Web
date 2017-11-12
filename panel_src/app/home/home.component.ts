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
    latitud: -31.335335,
    longitud: -64.303113,
    visitasFrecuentesDataBar: [],
    visitasActualDataBar: [],
    amenitiesDataPie: []
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

  // drawVisitsGraph() {
  //   var currentDate = new Date();
  //   for (var index = 0; index < 11; index++) {
  //     var previous_date = currentDate.getDate() - index;
  //     console.log(previous_date);
  //     this.barChartLabels.push(previous_date.toString());
  //   }
  //   console.log(this.barChartLabels);

  //   let clone = JSON.parse(JSON.stringify(this.barChartData));
  //   clone[0].data = [2, 5, 5, 4, 4, 5, 4, 8, 11, 12, 4];
  //   clone[1].data = [2, 5, 5, 4, 4, 5, 4, 8, 11, 12, 4];
  //   this.barChartData = clone;
  // }

  // Gráfico de torta para tipos amenities más reservados
  public pieChartLabels: string[] = [
    "Salón",
    "Canchas de Tenis",
    "Canchas de Fútbol"
  ];
  public pieChartData: number[] = [300, 500, 100];
  public pieChartType: string = "pie";

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
          // this.drawVisitsGraph();
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
        console.log(this.adminDashboard);
      }
    });
  }

  ngOnInit() {
    this.getUsuario();
  }
}
