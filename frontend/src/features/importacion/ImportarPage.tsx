import { useState, useRef } from 'react';
import { useImportarEmpleados } from './useImportarEmpleados';
import { useUserRole } from '../../core/auth/useUserRole';
import type { ImportResult } from './types';
import './importar.css';

export function ImportarPage() {
  const { isEditor } = useUserRole();
  const importar = useImportarEmpleados();
  const inputRef = useRef<HTMLInputElement>(null);

  const [archivo, setArchivo] = useState<File | null>(null);
  const [resultado, setResultado] = useState<ImportResult | null>(null);
  const [error, setError] = useState<string | null>(null);

  if (!isEditor) return <p>No tienes permiso para importar empleados.</p>;

  function pick(file: File | null) {
    setArchivo(file);
    setResultado(null);
    setError(null);
  }

  async function subir() {
    if (!archivo) return;
    setError(null);
    try {
      const res = await importar.mutateAsync(archivo);
      setResultado(res);
    } catch {
      setError('No se pudo importar el archivo. Verifica que sea el Excel correcto.');
    }
  }

  return (
    <div className="import-page">
      <div className="import-card">
        <div className="import-card-head">Importar empleados desde Excel</div>
        <div className="import-card-body">
          <div
            className="dropzone"
            onClick={() => inputRef.current?.click()}
            onDragOver={(e) => e.preventDefault()}
            onDrop={(e) => {
              e.preventDefault();
              pick(e.dataTransfer.files[0] ?? null);
            }}
          >
            <div className="dropzone-icon">⭱</div>
            <div className="dropzone-title">
              {archivo ? archivo.name : 'Haz clic o arrastra tu archivo Roster.xlsx aqui'}
            </div>
            <div className="dropzone-sub">El sistema valida cada fila antes de cargar</div>
            <input
              ref={inputRef}
              type="file"
              accept=".xlsx,.xls"
              style={{ display: 'none' }}
              onChange={(e) => pick(e.target.files?.[0] ?? null)}
            />
          </div>

          <div className="import-actions">
            <button
              className="btn-primary"
              disabled={!archivo || importar.isPending}
              onClick={() => void subir()}
            >
              {importar.isPending ? 'Importando...' : 'Importar'}
            </button>
          </div>

          {error && <p className="import-error">{error}</p>}

          {resultado && (
            <div className="import-result">
              <div className="result-summary">
                <div className="sum-box ok">
                  <div className="sum-n">{resultado.insertados}</div>
                  <div className="sum-l">Insertados</div>
                </div>
                <div className="sum-box info">
                  <div className="sum-n">{resultado.actualizados}</div>
                  <div className="sum-l">Actualizados</div>
                </div>
                <div className="sum-box warn">
                  <div className="sum-n">{resultado.omitidos}</div>
                  <div className="sum-l">Omitidos</div>
                </div>
                <div className="sum-box total">
                  <div className="sum-n">{resultado.totalFilas}</div>
                  <div className="sum-l">Total filas</div>
                </div>
              </div>

              {resultado.errores.length > 0 && (
                <div className="error-list">
                  <div className="error-head">Filas con error</div>
                  {resultado.errores.map((e, i) => (
                    <div key={i} className="error-item">
                      <span className="error-fila">Fila {e.fila}</span>
                      <span>{e.motivo}</span>
                    </div>
                  ))}
                </div>
              )}
            </div>
          )}
        </div>
      </div>
    </div>
  );
}
