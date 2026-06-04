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

export interface EmpleadoFormData {
  nombreCompleto: string;
  correo: string;
  direccion: string;
  telefono1: string;
  telefono2: string;
  fechaNacimiento: string;
  puesto: string;
  modalidad: string;
  jefeInmediato: string;
  facturable: boolean;
  fechaIngreso: string;
  salarioActual: string;
  moneda: string;
  paisId: string;
  departamentoId: string;
  activo: boolean;
}
