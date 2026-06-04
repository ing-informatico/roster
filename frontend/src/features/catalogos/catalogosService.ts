import { httpClient } from '../../core/http/httpClient';
import type { CatalogoItem } from './types';

type CatalogoTipo = 'departamentos' | 'paises';

export const catalogosService = {
  async getAll(tipo: CatalogoTipo): Promise<CatalogoItem[]> {
    const { data } = await httpClient.get<CatalogoItem[]>(`/api/${tipo}`);
    return data;
  },

  async create(tipo: CatalogoTipo, nombre: string): Promise<CatalogoItem> {
    const { data } = await httpClient.post<CatalogoItem>(`/api/${tipo}`, { nombre });
    return data;
  },

  async update(tipo: CatalogoTipo, id: number, nombre: string): Promise<void> {
    await httpClient.put(`/api/${tipo}/${id}`, { nombre });
  },

  async remove(tipo: CatalogoTipo, id: number): Promise<void> {
    await httpClient.delete(`/api/${tipo}/${id}`);
  },
};
