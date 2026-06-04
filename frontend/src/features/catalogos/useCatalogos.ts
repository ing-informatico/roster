import { useQuery } from '@tanstack/react-query';
import { catalogosService } from './catalogosService';

export function useDepartamentos() {
  return useQuery({
    queryKey: ['departamentos'],
    queryFn: () => catalogosService.getDepartamentos(),
    staleTime: 1000 * 60 * 10,
  });
}

export function usePaises() {
  return useQuery({
    queryKey: ['paises'],
    queryFn: () => catalogosService.getPaises(),
    staleTime: 1000 * 60 * 10,
  });
}
