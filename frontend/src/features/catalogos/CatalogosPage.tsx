import { useState } from 'react';
import { CatalogoManager } from './CatalogoManager';
import './catalogos.css';

type Tab = 'departamentos' | 'paises';

export function CatalogosPage() {
  const [tab, setTab] = useState<Tab>('departamentos');

  return (
    <div className="cat-page">
      <div className="cat-tabs">
        <button
          className={`cat-tab${tab === 'departamentos' ? ' active' : ''}`}
          onClick={() => setTab('departamentos')}
        >
          Departamentos
        </button>
        <button
          className={`cat-tab${tab === 'paises' ? ' active' : ''}`}
          onClick={() => setTab('paises')}
        >
          Paises
        </button>
      </div>

      {tab === 'departamentos' ? (
        <CatalogoManager tipo="departamentos" singular="Departamento" />
      ) : (
        <CatalogoManager tipo="paises" singular="Pais" />
      )}
    </div>
  );
}
