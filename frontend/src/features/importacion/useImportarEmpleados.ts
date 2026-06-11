import { useMutation, useQueryClient } from '@tanstack/react-query';
import { importacionService } from './importacionService';

export function useImportarEmpleados() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: (archivo: File) => importacionService.importarEmpleados(archivo),
    onSuccess: () => {
      void qc.invalidateQueries({ queryKey: ['empleados'] });
    },
  });
}
