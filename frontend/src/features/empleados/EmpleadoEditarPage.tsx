import { useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { useEmpleado } from './useEmpleado';
import { useActualizarEmpleado } from './useActualizarEmpleado';
import { useCatalogo } from '../catalogos/useCatalogos';
import { useUserRole } from '../../core/auth/useUserRole';
import type { EmpleadoDetalle, EmpleadoFormData } from './types';
import type { CatalogoItem } from '../catalogos/types';
import './empleadoEditar.css';

const MODALIDADES = ['WFH', 'Hibrido', 'Presencial'];

function buildInitialForm(
  empleado: EmpleadoDetalle,
  departamentos: CatalogoItem[],
  paises: CatalogoItem[],
): EmpleadoFormData {
  return {
    nombreCompleto: empleado.nombreCompleto,
    correo: empleado.correo ?? '',
    direccion: empleado.direccion ?? '',
    telefono1: empleado.telefono1 ?? '',
    telefono2: empleado.telefono2 ?? '',
    fechaNacimiento: empleado.fechaNacimiento ?? '',
    puesto: empleado.puesto ?? '',
    modalidad: empleado.modalidad ?? '',
    jefeInmediato: empleado.jefeInmediato ?? '',
    facturable: empleado.facturable,
    fechaIngreso: empleado.fechaIngreso ?? '',
    salarioActual: empleado.salarioActual?.toString() ?? '',
    moneda: empleado.moneda ?? 'USD',
    paisId: paises.find((p) => p.nombre === empleado.pais)?.id.toString() ?? '',
    departamentoId: departamentos.find((d) => d.nombre === empleado.departamento)?.id.toString() ?? '',
    activo: empleado.activo,
  };
}

interface FormProps {
  empleadoId: number;
  initial: EmpleadoFormData;
  departamentos: CatalogoItem[];
  paises: CatalogoItem[];
}

function EmpleadoForm({ empleadoId, initial, departamentos, paises }: FormProps) {
  const navigate = useNavigate();
  const actualizar = useActualizarEmpleado(empleadoId);
  const [form, setForm] = useState<EmpleadoFormData>(initial);
  const [errors, setErrors] = useState<Record<string, string>>({});
  const [saveError, setSaveError] = useState<string | null>(null);

  function set<K extends keyof EmpleadoFormData>(key: K, value: EmpleadoFormData[K]) {
    setForm((f) => ({ ...f, [key]: value }));
  }

  function validate(): boolean {
    const e: Record<string, string> = {};
    if (form.nombreCompleto.trim().length < 5)
      e.nombreCompleto = 'El nombre es obligatorio (minimo 5 caracteres).';
    if (form.correo && !form.correo.includes('@')) e.correo = 'El correo no es valido.';
    if (form.salarioActual && Number.isNaN(Number(form.salarioActual)))
      e.salarioActual = 'El salario debe ser un numero.';
    setErrors(e);
    return Object.keys(e).length === 0;
  }

  async function submit() {
    setSaveError(null);
    if (!validate()) return;
    try {
      await actualizar.mutateAsync({
        nombreCompleto: form.nombreCompleto.trim(),
        correo: form.correo.trim() || null,
        direccion: form.direccion.trim() || null,
        telefono1: form.telefono1.trim() || null,
        telefono2: form.telefono2.trim() || null,
        fechaNacimiento: form.fechaNacimiento || null,
        puesto: form.puesto.trim() || null,
        modalidad: form.modalidad || null,
        jefeInmediato: form.jefeInmediato.trim() || null,
        facturable: form.facturable,
        fechaIngreso: form.fechaIngreso || null,
        salarioActual: form.salarioActual ? Number(form.salarioActual) : null,
        moneda: form.moneda || 'USD',
        paisId: form.paisId ? Number(form.paisId) : null,
        departamentoId: form.departamentoId ? Number(form.departamentoId) : null,
        activo: form.activo,
      });
      navigate(`/empleados/${empleadoId}`);
    } catch {
      setSaveError('No se pudieron guardar los cambios. Intenta de nuevo.');
    }
  }

  return (
    <div className="emp-form">
      <button className="btn-ghost" onClick={() => navigate(`/empleados/${empleadoId}`)}>
        ‹ Cancelar
      </button>

      <div className="form-card">
        <div className="form-card-head">Editar empleado</div>
        <div className="form-grid">
          <div className="form-field full">
            <label>Nombre completo *</label>
            <input value={form.nombreCompleto} onChange={(e) => set('nombreCompleto', e.target.value)} />
            {errors.nombreCompleto && <span className="form-err">{errors.nombreCompleto}</span>}
          </div>

          <div className="form-field">
            <label>Correo corporativo</label>
            <input value={form.correo} onChange={(e) => set('correo', e.target.value)} />
            {errors.correo && <span className="form-err">{errors.correo}</span>}
          </div>
          <div className="form-field">
            <label>Puesto</label>
            <input value={form.puesto} onChange={(e) => set('puesto', e.target.value)} />
          </div>

          <div className="form-field">
            <label>Departamento</label>
            <select value={form.departamentoId} onChange={(e) => set('departamentoId', e.target.value)}>
              <option value="">— Seleccionar —</option>
              {departamentos.map((d) => (
                <option key={d.id} value={d.id}>{d.nombre}</option>
              ))}
            </select>
          </div>
          <div className="form-field">
            <label>Pais</label>
            <select value={form.paisId} onChange={(e) => set('paisId', e.target.value)}>
              <option value="">— Seleccionar —</option>
              {paises.map((p) => (
                <option key={p.id} value={p.id}>{p.nombre}</option>
              ))}
            </select>
          </div>

          <div className="form-field">
            <label>Modalidad</label>
            <select value={form.modalidad} onChange={(e) => set('modalidad', e.target.value)}>
              <option value="">— Seleccionar —</option>
              {MODALIDADES.map((m) => (
                <option key={m} value={m}>{m}</option>
              ))}
            </select>
          </div>
          <div className="form-field">
            <label>Jefe inmediato</label>
            <input value={form.jefeInmediato} onChange={(e) => set('jefeInmediato', e.target.value)} />
          </div>

          <div className="form-field">
            <label>Direccion</label>
            <input value={form.direccion} onChange={(e) => set('direccion', e.target.value)} />
          </div>
          <div className="form-field">
            <label>Telefono 1</label>
            <input value={form.telefono1} onChange={(e) => set('telefono1', e.target.value)} />
          </div>
          <div className="form-field">
            <label>Telefono 2</label>
            <input value={form.telefono2} onChange={(e) => set('telefono2', e.target.value)} />
          </div>

          <div className="form-field">
            <label>Fecha de ingreso</label>
            <input type="date" value={form.fechaIngreso} onChange={(e) => set('fechaIngreso', e.target.value)} />
          </div>
          <div className="form-field">
            <label>Fecha de nacimiento</label>
            <input type="date" value={form.fechaNacimiento} onChange={(e) => set('fechaNacimiento', e.target.value)} />
          </div>

          <div className="form-field">
            <label>Salario actual</label>
            <input value={form.salarioActual} onChange={(e) => set('salarioActual', e.target.value)} />
            {errors.salarioActual && <span className="form-err">{errors.salarioActual}</span>}
          </div>
          <div className="form-field">
            <label>Moneda</label>
            <input value={form.moneda} onChange={(e) => set('moneda', e.target.value)} />
          </div>

          <div className="form-field full">
            <label>Opciones</label>
            <div className="form-checks">
              <label className="check">
                <input type="checkbox" checked={form.facturable} onChange={(e) => set('facturable', e.target.checked)} />
                Facturable (Billable)
              </label>
              <label className="check">
                <input type="checkbox" checked={form.activo} onChange={(e) => set('activo', e.target.checked)} />
                Activo
              </label>
            </div>
          </div>
        </div>

        {saveError && <p className="form-err form-err-foot">{saveError}</p>}

        <div className="form-foot">
          <button className="btn-ghost" onClick={() => navigate(`/empleados/${empleadoId}`)}>Cancelar</button>
          <button className="btn-primary" onClick={() => void submit()}>Guardar cambios</button>
        </div>
      </div>
    </div>
  );
}

export function EmpleadoEditarPage() {
  const { id } = useParams<{ id: string }>();
  const { isEditor } = useUserRole();
  const numericId = Number(id);

  const { data: empleado, isLoading } = useEmpleado(numericId);
  const { data: departamentos } = useCatalogo('departamentos');
  const { data: paises } = useCatalogo('paises');

  if (!isEditor) return <p>No tienes permiso para editar empleados.</p>;
  if (isLoading || !empleado || !departamentos || !paises) return <p>Cargando...</p>;

  const initial = buildInitialForm(empleado, departamentos, paises);

  return (
    <EmpleadoForm
      empleadoId={numericId}
      initial={initial}
      departamentos={departamentos}
      paises={paises}
    />
  );
}
