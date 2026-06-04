import { useQuery } from '@tanstack/react-query';
import { empleadosService } from './empleadosService';

export function useEmpleado(id: number) {
  return useQuery({
    queryKey: ['empleado', id],
    queryFn: () => empleadosService.getById(id),
    enabled: Number.isFinite(id),
  });
}
