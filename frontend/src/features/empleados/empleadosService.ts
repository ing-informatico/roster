import { httpClient } from '../../core/http/httpClient';
import type { EmpleadoListItem, EmpleadoDetalle } from './types';

export const empleadosService = {
  async getAll(): Promise<EmpleadoListItem[]> {
    const { data } = await httpClient.get<EmpleadoListItem[]>('/api/empleados');
    return data;
  },

  async getById(id: number): Promise<EmpleadoDetalle> {
    const { data } = await httpClient.get<EmpleadoDetalle>(`/api/empleados/${id}`);
    return data;
  },
};
