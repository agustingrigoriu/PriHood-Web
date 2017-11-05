
export interface RootDashboard {
  cantidad_barrios: number;
}

export interface AdminDashboard {
  cantidad_residencias: number;
  cantidad_residentes: number;
  latitud: number;
  longitud: number;
  visitas_frecuentes: [number];
  visitas_actuales : [number];
}