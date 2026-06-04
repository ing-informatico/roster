import { httpClient } from '../../core/http/httpClient';
import type { CatalogoItem } from './types';

export const catalogosService = {
  async getDepartamentos(): Promise<CatalogoItem[]> {
    const { data } = await httpClient.get<CatalogoItem[]>('/api/departamentos');
    return data;
  },

  async getPaises(): Promise<CatalogoItem[]> {
    const { data } = await httpClient.get<CatalogoItem[]>('/api/paises');
    return data;
  },
};
