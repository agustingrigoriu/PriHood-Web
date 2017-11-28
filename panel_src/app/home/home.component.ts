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
    visitasDataBar: [],
    amenitiesDataPie: [],
    recaudacionReservasLine: []
  };

  //bar Chart Visitas (Frecuentes y Actuales)
  public barChartOptions: any = {
    scaleShowVerticalLines: false,
    responsive: true,
    scales: {
      yAxes: [{id: 'y-axis-1', type: 'linear', position: 'left', ticks: {min: 0}}]
    }
  };
  public barChartType: string = "bar";
  public barChartLegend: boolean = true;

  public barChartLabels: string[] = [];
  public barChartData: any[] = [{ data: [0], label: "Visitas" }];

  // Gráfico de torta para tipos amenities más reservados
  public pieChartLabels: string[] = [];
  public pieChartData: number[] = [];
  public pieChartType: string = "pie";
  public pieChartColors: any[] = [
    {
      backgroundColor: [
        "#b8436d",
        "#00d9f9",
        "#a4c73c",
        "#a4add3",
        "#511730",
        "#8e443d",
        "#e0d68a"
      ]
    }
  ];

  // Line chart para recaudaciones de amenities por mes
  public lineChartData: Array<any> = [{ data: [], label: "Recaudaciones" }];
  public lineChartLabels: Array<any> = [];
  public lineChartOptions: any = {
    responsive: true,
    maintainAspectRatio: true
  };
  public lineChartColors: Array<any> = [
    {
      // grey
      backgroundColor: "rgba(148,159,177,0.2)",
      borderColor: "rgba(148,159,177,1)",
      pointBackgroundColor: "rgba(148,159,177,1)",
      pointBorderColor: "#fff",
      pointHoverBackgroundColor: "#fff",
      pointHoverBorderColor: "rgba(148,159,177,0.8)"
    },
    {
      // dark grey
      backgroundColor: "rgba(77,83,96,0.2)",
      borderColor: "rgba(77,83,96,1)",
      pointBackgroundColor: "rgba(77,83,96,1)",
      pointBorderColor: "#fff",
      pointHoverBackgroundColor: "#fff",
      pointHoverBorderColor: "rgba(77,83,96,1)"
    },
    {
      // grey
      backgroundColor: "rgba(148,159,177,0.2)",
      borderColor: "rgba(148,159,177,1)",
      pointBackgroundColor: "rgba(148,159,177,1)",
      pointBorderColor: "#fff",
      pointHoverBackgroundColor: "#fff",
      pointHoverBorderColor: "rgba(148,159,177,0.8)"
    }
  ];
  public lineChartLegend: boolean = true;
  public lineChartType: string = "line";

  noHayDatosVisitsGraphBar() {
    return this.adminDashboard.visitasDataBar.length === 0;
  }

  noHayDatosAmenitiesPie() {
    return this.adminDashboard.amenitiesDataPie.length === 0;
  }

  noHayDatosAmenitiesLine() {
    return this.adminDashboard.recaudacionReservasLine.length === 0;
  }

  drawVisitsGraphBar() {
    let clone_data = JSON.parse(JSON.stringify(this.barChartData));
    this.barChartLabels.length = 0;
    var labels = this.adminDashboard.visitasDataBar.map(a => a.label);
    for (let i = labels.length - 1; i >= 0; i--) {
      this.barChartLabels.push(labels[i]);
    }
    clone_data[0].data = this.adminDashboard.visitasDataBar.map(a => a.count);
    this.barChartData[0] = clone_data[0];

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
    this.lineChartData[0].data = clone_data[0];
  }

  // events
  public pieChartClicked(e: any): void {
    console.log(e);
  }

  public pieChartHovered(e: any): void {
    console.log(e);
  }
  public barChartClicked(e: any): void {
    console.log(e);
  }

  public barChartHovered(e: any): void {
    console.log(e);
  }
  public lineChartClicked(e: any): void {
    console.log(e);
  }

  public lineChartHovered(e: any): void {
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
        console.log(this.adminDashboard);
        if (this.adminDashboard.amenitiesDataPie.length > 0)
          this.drawAmenitiesPie();
        if (this.adminDashboard.visitasDataBar.length > 0)
          this.drawVisitsGraphBar();
        if (this.adminDashboard.recaudacionReservasLine.length > 0)
          this.drawAmenitiesLine();
      }
    });
  }

  ngOnInit() {
    this.getUsuario();
  }
}
