import { httpClient } from '../../core/http/httpClient';
import type { EmpleadoListItem, EmpleadoDetalle } from './types';

export interface ActualizarEmpleadoPayload {
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
  salarioActual: number | null;
  moneda: string;
  paisId: number | null;
  departamentoId: number | null;
  activo: boolean;
}

export const empleadosService = {
  async getAll(): Promise<EmpleadoListItem[]> {
    const { data } = await httpClient.get<EmpleadoListItem[]>('/api/empleados');
    return data;
  },

  async getById(id: number): Promise<EmpleadoDetalle> {
    const { data } = await httpClient.get<EmpleadoDetalle>(`/api/empleados/${id}`);
    return data;
  },

  async update(id: number, payload: ActualizarEmpleadoPayload): Promise<void> {
    await httpClient.put(`/api/empleados/${id}`, payload);
  },
};
