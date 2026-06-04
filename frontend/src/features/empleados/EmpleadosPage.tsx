import { useEmpleados } from './useEmpleados';
import { DataTable, type Column } from '../../shared/components/DataTable';
import { Pill } from '../../shared/components/Pill';
import type { EmpleadoListItem } from './types';

const columns: Column<EmpleadoListItem>[] = [
  { key: 'codigo', header: 'Codigo', render: (e) => `#${e.codigo}` },
  { key: 'nombre', header: 'Nombre', render: (e) => e.nombreCompleto },
  { key: 'correo', header: 'Correo', render: (e) => e.correo ?? '—' },
  { key: 'departamento', header: 'Departamento', render: (e) => e.departamento ?? '—' },
  { key: 'pais', header: 'Pais', render: (e) => e.pais ?? '—' },
  {
    key: 'estado',
    header: 'Estado',
    render: (e) => (
      <Pill tone={e.activo ? 'green' : 'gray'} label={e.activo ? 'Activo' : 'Inactivo'} />
    ),
  },
];

export function EmpleadosPage() {
  const { data, isLoading, isError } = useEmpleados();

  if (isLoading) return <p>Cargando empleados...</p>;
  if (isError) return <p>Error al cargar los empleados.</p>;

  return (
    <DataTable
      columns={columns}
      rows={data ?? []}
      getRowKey={(e) => e.id}
      emptyMessage="No hay empleados registrados."
    />
  );
}
