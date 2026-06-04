import { useState } from 'react';
import {
  useCatalogo,
  useCrearCatalogo,
  useActualizarCatalogo,
  useEliminarCatalogo,
} from './useCatalogos';
import { DataTable, type Column } from '../../shared/components/DataTable';
import { Modal } from '../../shared/components/Modal';
import { ConfirmDialog } from '../../shared/components/ConfirmDialog';
import { useUserRole } from '../../core/auth/useUserRole';
import type { CatalogoItem } from './types';

type CatalogoTipo = 'departamentos' | 'paises';

interface CatalogoManagerProps {
  tipo: CatalogoTipo;
  singular: string;
}

export function CatalogoManager({ tipo, singular }: CatalogoManagerProps) {
  const { isEditor } = useUserRole();
  const { data, isLoading, isError } = useCatalogo(tipo);
  const crear = useCrearCatalogo(tipo);
  const actualizar = useActualizarCatalogo(tipo);
  const eliminar = useEliminarCatalogo(tipo);

  const [modalOpen, setModalOpen] = useState(false);
  const [editing, setEditing] = useState<CatalogoItem | null>(null);
  const [nombre, setNombre] = useState('');
  const [error, setError] = useState<string | null>(null);

  const [toDelete, setToDelete] = useState<CatalogoItem | null>(null);
  const [deleteError, setDeleteError] = useState<string | null>(null);

  function openCreate() {
    setEditing(null);
    setNombre('');
    setError(null);
    setModalOpen(true);
  }

  function openEdit(item: CatalogoItem) {
    setEditing(item);
    setNombre(item.nombre);
    setError(null);
    setModalOpen(true);
  }

  async function save() {
    const value = nombre.trim();
    if (value.length < 2) {
      setError('El nombre es obligatorio (minimo 2 caracteres).');
      return;
    }
    try {
      if (editing) {
        await actualizar.mutateAsync({ id: editing.id, nombre: value });
      } else {
        await crear.mutateAsync(value);
      }
      setModalOpen(false);
    } catch {
      setError('No se pudo guardar. Es posible que el nombre ya exista.');
    }
  }

  async function confirmDelete() {
    if (!toDelete) return;
    try {
      await eliminar.mutateAsync(toDelete.id);
      setToDelete(null);
    } catch {
      setDeleteError(
        `No se puede eliminar "${toDelete.nombre}": hay empleados que usan este valor.`,
      );
      setToDelete(null);
    }
  }

  const columns: Column<CatalogoItem>[] = [
    { key: 'nombre', header: singular, render: (c) => c.nombre },
    {
      key: 'acciones',
      header: '',
      render: (c) =>
        isEditor ? (
          <div className="cat-actions">
            <button className="action-btn" onClick={() => openEdit(c)}>
              Editar
            </button>
            <button
              className="action-btn action-btn-danger"
              onClick={() => {
                setDeleteError(null);
                setToDelete(c);
              }}
            >
              Eliminar
            </button>
          </div>
        ) : null,
    },
  ];

  if (isLoading) return <p>Cargando...</p>;
  if (isError) return <p>Error al cargar el catalogo.</p>;

  return (
    <div className="cat-block">
      {isEditor && (
        <div className="cat-toolbar">
          <button className="btn-primary" onClick={openCreate}>
            + Nuevo
          </button>
        </div>
      )}

      <DataTable
        columns={columns}
        rows={data ?? []}
        getRowKey={(c) => c.id}
        emptyMessage="Sin registros."
      />

      <Modal
        open={modalOpen}
        title={editing ? `Editar ${singular.toLowerCase()}` : `Nuevo ${singular.toLowerCase()}`}
        onClose={() => setModalOpen(false)}
        footer={
          <>
            <button className="btn-ghost" onClick={() => setModalOpen(false)}>
              Cancelar
            </button>
            <button className="btn-primary" onClick={() => void save()}>
              Guardar
            </button>
          </>
        }
      >
        <label className="field-label" htmlFor="cat-nombre">
          Nombre
        </label>
        <input
          id="cat-nombre"
          className="field-input"
          value={nombre}
          onChange={(e) => setNombre(e.target.value)}
          placeholder={`Nombre del ${singular.toLowerCase()}`}
          autoFocus
        />
        {error && <p className="field-error">{error}</p>}
      </Modal>

      <ConfirmDialog
        open={toDelete !== null}
        title={`Eliminar ${singular.toLowerCase()}`}
        message={`Estas seguro de eliminar "${toDelete?.nombre}"? Esta accion no se puede deshacer.`}
        confirmLabel="Eliminar"
        danger
        onConfirm={() => void confirmDelete()}
        onCancel={() => setToDelete(null)}
      />

      <ConfirmDialog
        open={deleteError !== null}
        title="No se puede eliminar"
        message={deleteError ?? ''}
        confirmLabel="Entendido"
        onConfirm={() => setDeleteError(null)}
        onCancel={() => setDeleteError(null)}
      />
    </div>
  );
}
