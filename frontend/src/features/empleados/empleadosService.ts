import { httpClient } from '../../core/http/httpClient';
import type { EmpleadoListItem } from './types';

export const empleadosService = {
  async getAll(): Promise<EmpleadoListItem[]> {
    const { data } = await httpClient.get<EmpleadoListItem[]>('/api/empleados');
    return data;
  },
};
