import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { catalogosService } from './catalogosService';

type CatalogoTipo = 'departamentos' | 'paises';

export function useCatalogo(tipo: CatalogoTipo) {
  return useQuery({
    queryKey: [tipo],
    queryFn: () => catalogosService.getAll(tipo),
    staleTime: 1000 * 60 * 10,
  });
}

export function useCrearCatalogo(tipo: CatalogoTipo) {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: (nombre: string) => catalogosService.create(tipo, nombre),
    onSuccess: () => qc.invalidateQueries({ queryKey: [tipo] }),
  });
}

export function useActualizarCatalogo(tipo: CatalogoTipo) {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: ({ id, nombre }: { id: number; nombre: string }) =>
      catalogosService.update(tipo, id, nombre),
    onSuccess: () => qc.invalidateQueries({ queryKey: [tipo] }),
  });
}

export function useEliminarCatalogo(tipo: CatalogoTipo) {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: (id: number) => catalogosService.remove(tipo, id),
    onSuccess: () => qc.invalidateQueries({ queryKey: [tipo] }),
  });
}
