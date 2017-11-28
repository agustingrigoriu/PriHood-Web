import { Component, OnInit } from "@angular/core";
import { LoginService } from "../../services/login.service";
import { Usuario } from "../usuarios/usuario.model";

@Component({
  selector: "app-usuario",
  templateUrl: "./usuario.component.html",
  styleUrls: ["./usuario.component.css"]
})
export class UsuarioComponent implements OnInit {
  constructor(protected LoginService: LoginService) {}

  public usuario: Usuario = {
    email: "",
    idPerfil: -1,
    nombre: "",
    apellido: "",
    perfil: "",
    barrio: "",
    avatar: ""
  };

  async ngOnInit() {
    const { error, data: usuario } = await this.LoginService.getUserLogin();

    if (error || !usuario) {
      throw "Usuario no logueado.";
    }

    this.usuario = usuario;
    console.log(this.usuario);
  }
}
