import { useEmpleados } from './useEmpleados';

export function EmpleadosPage() {
  const { data, isLoading, isError } = useEmpleados();

  if (isLoading) return <p>Cargando empleados...</p>;
  if (isError) return <p>Error al cargar los empleados.</p>;

  return (
    <div>
      <h2>Empleados</h2>
      <table>
        <thead>
          <tr>
            <th>Codigo</th>
            <th>Nombre</th>
            <th>Correo</th>
            <th>Departamento</th>
            <th>Pais</th>
            <th>Estado</th>
          </tr>
        </thead>
        <tbody>
          {data?.map((e) => (
            <tr key={e.id}>
              <td>{e.codigo}</td>
              <td>{e.nombreCompleto}</td>
              <td>{e.correo ?? '—'}</td>
              <td>{e.departamento ?? '—'}</td>
              <td>{e.pais ?? '—'}</td>
              <td>{e.activo ? 'Activo' : 'Inactivo'}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
