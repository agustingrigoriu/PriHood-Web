import { Component, OnInit } from '@angular/core';
import { AmenitiesService } from './amenities.service';
import { Amenity } from './amenity.model';
import { NgForm } from "@angular/forms/forms";
import { NotificationsService } from 'angular2-notifications';
import { ConfirmationService } from '@jaspero/ng2-confirmations';
import { Validators } from '@angular/forms';
import { FormControl } from '@angular/forms';
import { FormGroup } from '@angular/forms';

@Component({
    selector: 'app-amenities',
    templateUrl: './amenities.component.html',
    styleUrls: ['./amenities.component.css']
})

export class AmenitiesComponent implements OnInit {
    ameForm: any;
    constructor(protected AmenitiesService: AmenitiesService, private notificaciones: NotificationsService, private confirmacion: ConfirmationService) { }

    amenities: Amenity[] = [];
    cargando: boolean;
    p: number = 1;

    amenity: Amenity = {
        nombre: '',
        descripcion: '',
        ubicacion: '',
        idTipoAmenity: 1,
    };
    amenitySeleccionada: Amenity;

    tipos = [
        { name: 'Cancha de tenis', id: 1 },
        { name: 'Cancha de fútbol', id: 2 },
        { name: 'Cancha de paddle', id: 3 },
        { name: 'Salón', id: 4 },
        { name: 'Piscinas', id: 5 }
    ];

    borrarAmenity(amenity: Amenity): void {
        this.cargando = true;
        this.AmenitiesService.deleteAmenity(amenity.id).then(response => {
            this.cargando = false;
            if (response.error) {
                this.notificaciones.error("Error", "No se pudo borrar el amenity");
            } else {
                this.actualizarListado();
                this.notificaciones.success("Éxito", "Se borró correctamente el amenity");
            }
        });
    }

    crearAmenity(amenity: Amenity, form: NgForm) {
        this.cargando = true;
        this.AmenitiesService.crearAmenity(amenity).then(response => {
            this.cargando = false;
            if (response.error) {
                this.notificaciones.error("Error", "No se pudo crear el amenity");
            } else {
                this.actualizarListado();
                form.reset();
                this.notificaciones.success("Éxito", "Se creó correctamente el amenity");
            }
        });
    }

    onSelectedAme(amenity: Amenity) {
        this.amenitySeleccionada = { ...amenity };
    }

    modificarAmenity(amenity: Amenity) {
        this.cargando = true;
        const amenityUpdate: Amenity = {
            descripcion: amenity.descripcion,
            idTipoAmenity: amenity.idTipoAmenity,
            ubicacion: amenity.ubicacion,
            nombre: amenity.nombre
        };
        this.AmenitiesService.updateAmenity(amenity.id, amenityUpdate).then(response => {
            this.cargando = false;
            if (response.error) {
                this.notificaciones.error("Error", "No se pudo modificar rl amenity");
            } else {
                this.notificaciones.success("Éxito", "Se modificó correctamente el amenity");
                this.actualizarListado();
            }
        });
    }


    cargarTurnos(idAmenity: number) {

    }

    verTurnos(idAmenity: number) {

    }

    actualizarListado() {
        this.cargando = true;
        this.AmenitiesService.getAllAmenities().then(response => {
            this.amenities = response.data;
            this.cargando = false;
        });
    }

    ngOnInit(): void {
        this.actualizarListado();
        this.ameForm = new FormGroup({
            'name': new FormControl(this.amenity.nombre, [
                Validators.required,
            ]),

        });
    }

    confirmacionEliminar(amenity: Amenity) {
        this.confirmacion.create('Confirmación', '¿Está seguro qué desea borrar?', { showCloseButton: true, confirmText: "SI", declineText: "NO" })
            .subscribe((ans: any) => {
                if (ans.resolved) {
                    this.borrarAmenity(amenity);
                }
            });
    }
}