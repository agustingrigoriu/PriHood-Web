import { Barrio } from '../barrios/barrio.model';

export interface Empleado {
   id?:number; 
   apellido:string;
   nombre: string;
   fecha_nacimiento: Date;
   numero_documento: number;
   telefono: number; 
   id_perfil:number;
   id_tipo_documento:number;
   email: string;
   password: string;
}