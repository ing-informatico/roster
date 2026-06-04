export interface EmpleadoListItem {
  id: number;
  codigo: string;
  nombreCompleto: string;
  correo: string | null;
  departamento: string | null;
  pais: string | null;
  activo: boolean;
}

export interface EmpleadoDetalle {
  id: number;
  codigo: string;
  nombreCompleto: string;
  correo: string | null;
  direccion: string | null;
  telefono1: string | null;
  telefono2: string | null;
  fechaNacimiento: string | null;
  puesto: string | null;
  modalidad: string | null;
  jefeInmediato: string | null;
  facturable: boolean;
  fechaIngreso: string | null;
  activo: boolean;
  salarioActual: number | null;
  moneda: string;
  departamento: string | null;
  pais: string | null;
}
