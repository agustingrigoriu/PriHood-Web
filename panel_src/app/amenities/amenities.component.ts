import { Component, OnInit } from '@angular/core';
import { AmenitiesService } from './amenities.service';
import { Amenity } from './amenity.model';
import { NgForm } from "@angular/forms/forms";

@Component({
    selector: 'app-amenities',
    templateUrl: './amenities.component.html',
    styleUrls: ['./amenities.component.css']
})

export class AmenitiesComponent implements OnInit {
    constructor(protected AmenitiesService: AmenitiesService) { }

    amenities: Amenity[] = [];

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
        if (confirm('¿Borrar este amenity?')) {
            this.AmenitiesService.deleteAmenity(amenity.id).then(response => {
                if (response.error) {
                    alert('No se pudo borrar el Amenity');
                } else {
                    this.actualizarListado();
                    alert('Se borró correctamente');
                }
            });
        }
    }

    crearAmenity(amenity: Amenity, form: NgForm) {
        this.AmenitiesService.crearAmenity(amenity).then(response => {
            if (response.error) {
                alert('No se pudo crear');
            } else {
                this.actualizarListado();
                form.reset();
                alert('Se creó correctamente');
            }
        });
    }

    onSelectedAme(amenity: Amenity) {
        this.amenitySeleccionada = { ...amenity };
    }

    modificarAmenity(amenity: Amenity) {
        console.log(amenity);
        const amenityUpdate: Amenity = {
            descripcion: amenity.descripcion,
            idTipoAmenity: amenity.idTipoAmenity,
            ubicacion: amenity.ubicacion,
            nombre: amenity.nombre
        };
        this.AmenitiesService.updateAmenity(amenity.id, amenityUpdate).then(response => {
            if (response.error) {
                alert('No se pudo modificar');
            } else {
                alert('Se modificó correctamente')
                this.actualizarListado();
            }
        });
    }


    cargarTurnos(idAmenity: number) {

    }

    verTurnos(idAmenity: number) {

    }

    actualizarListado() {
        this.AmenitiesService.getAllAmenities().then(response => {
            this.amenities = response.data;
        });
    }

    ngOnInit(): void {
        this.actualizarListado();
    }
}