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
  numeroFila: number | null;
  codigo: string;
  nombreCompleto: string;
  genero: string | null;
  correo: string | null;
  direccion: string | null;
  telefono1: string | null;
  fechaNacimiento: string | null;
  padreOMadre: string | null;
  puesto: string | null;
  modalidad: string | null;
  modalidadCompensacion: string | null;
  site: string | null;
  jefeInmediato: string | null;
  teamLead: string | null;
  sdm: string | null;
  manager: string | null;
  facturable: boolean;
  fechaIngreso: string | null;
  activo: boolean;
  centroCosto: string | null;
  idCentroCosto: string | null;
  centroCostoUbicacion: string | null;
  proyecto: string | null;
  equipoAsignado: string | null;
  idObsPoliza: string | null;
  tipoSeguro: string | null;
  idBeneficioHospAngeles: string | null;
  salarioActual: number | null;
  moneda: string;
  departamento: string | null;
  pais: string | null;
}

export interface EmpleadoFormData {
  nombreCompleto: string;
  genero: string;
  correo: string;
  direccion: string;
  telefono1: string;
  fechaNacimiento: string;
  padreOMadre: string;
  puesto: string;
  modalidad: string;
  modalidadCompensacion: string;
  site: string;
  jefeInmediato: string;
  teamLead: string;
  sdm: string;
  manager: string;
  facturable: boolean;
  fechaIngreso: string;
  centroCosto: string;
  idCentroCosto: string;
  centroCostoUbicacion: string;
  proyecto: string;
  equipoAsignado: string;
  idObsPoliza: string;
  tipoSeguro: string;
  idBeneficioHospAngeles: string;
  salarioActual: string;
  moneda: string;
  paisId: string;
  departamentoId: string;
  activo: boolean;
}
