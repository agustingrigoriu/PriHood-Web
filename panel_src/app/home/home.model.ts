
export interface RootDashboard {
  cantidad_barrios: number;
}

export interface AdminDashboard {
  cantidad_residencias: number;
  cantidad_residentes: number;
  cantidad_amenities: number;
  latitud: number;
  longitud: number;
  visitasFrecuentesDataBar: any[];
  visitasActualDataBar: any[];
  amenitiesDataPie: any[];
}