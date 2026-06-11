import { httpClient } from '../../core/http/httpClient';
import type { ImportResult } from './types';

export const importacionService = {
  async importarEmpleados(archivo: File): Promise<ImportResult> {
    const formData = new FormData();
    formData.append('archivo', archivo);
    const { data } = await httpClient.post<ImportResult>(
      '/api/importacion/empleados',
      formData,
      { headers: { 'Content-Type': 'multipart/form-data' } },
    );
    return data;
  },
};
