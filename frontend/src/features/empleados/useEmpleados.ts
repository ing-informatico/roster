import { useQuery } from '@tanstack/react-query';
import { empleadosService } from './empleadosService';

export function useEmpleados() {
  return useQuery({
    queryKey: ['empleados'],
    queryFn: empleadosService.getAll,
  });
}
