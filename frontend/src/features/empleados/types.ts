export interface EmpleadoListItem {
  id: number;
  codigo: string;
  nombreCompleto: string;
  correo: string | null;
  departamento: string | null;
  pais: string | null;
  activo: boolean;
}
