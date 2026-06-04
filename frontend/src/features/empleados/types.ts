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
  fechaIngreso: string | null;
  fechaNacimiento: string | null;
  departamento: string | null;
  pais: string | null;
  activo: boolean;
}
