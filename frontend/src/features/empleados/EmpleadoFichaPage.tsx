import { useParams, useNavigate } from 'react-router-dom';
import { useEmpleado } from './useEmpleado';
import { useUserRole } from '../../core/auth/useUserRole';
import { Pill } from '../../shared/components/Pill';
import './empleadoFicha.css';

function initials(name: string): string {
  return name
    .split(' ')
    .slice(0, 2)
    .map((p) => p.charAt(0))
    .join('')
    .toUpperCase();
}

function money(value: number | null, moneda: string): string {
  if (value === null) return '—';
  return `${moneda} ${value.toLocaleString('en-US', { minimumFractionDigits: 0 })}`;
}

interface FieldProps {
  label: string;
  value: string | null | undefined;
}

function Field({ label, value }: FieldProps) {
  return (
    <div className="fd-item">
      <span className="fd-label">{label}</span>
      <span className="fd-value">{value ?? '—'}</span>
    </div>
  );
}

export function EmpleadoFichaPage() {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const { isEditor } = useUserRole();
  const numericId = Number(id);
  const { data: e, isLoading, isError } = useEmpleado(numericId);

  if (isLoading) return <p>Cargando ficha...</p>;
  if (isError || !e) return <p>No se pudo cargar la ficha del empleado.</p>;

  return (
    <div className="ficha">
      <button className="btn-ghost" onClick={() => navigate('/empleados')}>
        ‹ Volver a empleados
      </button>

      <div className="ficha-head">
        <div className="fh-avatar">{initials(e.nombreCompleto)}</div>
        <div className="fh-info">
          <div className="fh-name-row">
            <h2>{e.nombreCompleto}</h2>
            <Pill tone={e.activo ? 'green' : 'gray'} label={e.activo ? 'Activo' : 'Inactivo'} />
          </div>
          <div className="fh-sub">
            #{e.codigo} · {e.puesto ?? 'Sin puesto'} · {e.departamento ?? '—'}
          </div>
        </div>
        {isEditor && (
          <button className="btn-primary" onClick={() => navigate(`/empleados/${e.id}/editar`)}>
            Editar
          </button>
        )}
      </div>

      <div className="ficha-grid">
        <div className="ficha-card">
          <div className="ficha-card-head">Datos personales</div>
          <div className="ficha-card-body">
            <Field label="Nombre completo" value={e.nombreCompleto} />
            <Field label="Fecha de nacimiento" value={e.fechaNacimiento} />
            <Field label="Pais" value={e.pais} />
            <Field label="Direccion" value={e.direccion} />
            <Field label="Telefono 1" value={e.telefono1} />
            <Field label="Telefono 2" value={e.telefono2} />
            <Field label="Correo" value={e.correo} />
          </div>
        </div>

        <div className="ficha-card">
          <div className="ficha-card-head">Datos laborales</div>
          <div className="ficha-card-body">
            <Field label="Puesto actual" value={e.puesto} />
            <Field label="Departamento" value={e.departamento} />
            <Field label="Fecha de ingreso" value={e.fechaIngreso} />
            <Field label="Modalidad" value={e.modalidad} />
            <Field label="Jefe inmediato" value={e.jefeInmediato} />
            <Field label="Facturable" value={e.facturable ? 'Si (Billable)' : 'No'} />
          </div>
        </div>

        <div className="ficha-card">
          <div className="ficha-card-head">Compensacion</div>
          <div className="ficha-card-body">
            <Field label="Salario actual" value={money(e.salarioActual, e.moneda)} />
            <Field label="Moneda" value={e.moneda} />
          </div>
        </div>

        <div className="ficha-card">
          <div className="ficha-card-head">Historial</div>
          <div className="ficha-card-body">
            <p className="ficha-empty">
              Los historiales de puesto, salario, evaluaciones y bonos se cargaran con la
              importacion de Excel.
            </p>
          </div>
        </div>
      </div>
    </div>
  );
}
