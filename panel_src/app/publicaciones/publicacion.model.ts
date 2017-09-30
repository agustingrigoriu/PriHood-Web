export interface Publicacion {
  id?: number,
  titulo?: string,
  texto?: string,
  fecha?: Date,
  perfil?: string,
  id_residente?: number,
  residente?: {
    nombre: string,
    apellido: string
  },
  residencia?: {
    id: number,
    nombre: string,
    ubicacion: string
  }
}