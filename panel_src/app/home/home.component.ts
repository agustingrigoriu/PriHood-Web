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

  // Gráfico de torta para tipos amenities más reservados
  public pieChartLabels: string[] = [];
  public pieChartData: number[] = [];
  public pieChartType: string = "pie";

  drawVisitsGraphBar() {

    let clone_data = JSON.parse(JSON.stringify(this.barChartData));
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
    let clone_labels = JSON.parse(JSON.stringify(this.pieChartLabels));
    clone_data = this.adminDashboard.amenitiesDataPie.map(a => a.count);
    clone_labels = this.adminDashboard.amenitiesDataPie.map(a => a.label);
    this.pieChartData = clone_data;
    this.pieChartLabels = clone_labels;
    console.log(this.pieChartLabels);

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
      }
    });
  }

  ngOnInit() {
    this.getUsuario();
  }
}
