import { useMutation, useQueryClient } from '@tanstack/react-query';
import { empleadosService, type ActualizarEmpleadoPayload } from './empleadosService';

export function useActualizarEmpleado(id: number) {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: (payload: ActualizarEmpleadoPayload) => empleadosService.update(id, payload),
    onSuccess: () => {
      void qc.invalidateQueries({ queryKey: ['empleado', id] });
      void qc.invalidateQueries({ queryKey: ['empleados'] });
    },
  });
}
