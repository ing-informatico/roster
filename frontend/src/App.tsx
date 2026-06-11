import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import { AuthGuard } from './core/auth/AuthGuard';
import { AppShell } from './shared/layout/AppShell';
import { EmpleadosPage } from './features/empleados/EmpleadosPage';
import { EmpleadoFichaPage } from './features/empleados/EmpleadoFichaPage';
import { EmpleadoEditarPage } from './features/empleados/EmpleadoEditarPage';
import { CatalogosPage } from './features/catalogos/CatalogosPage';
import { ImportarPage } from './features/importacion/ImportarPage';

function App() {
  return (
    <AuthGuard>
      <BrowserRouter>
        <Routes>
          <Route
            path="/empleados"
            element={
              <AppShell activeKey="empleados" title="Empleados">
                <EmpleadosPage />
              </AppShell>
            }
          />
          <Route
            path="/empleados/:id"
            element={
              <AppShell activeKey="empleados" title="Ficha del empleado">
                <EmpleadoFichaPage />
              </AppShell>
            }
          />
          <Route
            path="/catalogos"
            element={
              <AppShell activeKey="catalogos" title="Catalogos">
                <CatalogosPage />
              </AppShell>
            }
          />
          <Route
            path="/empleados/:id/editar"
            element={
              <AppShell activeKey="empleados" title="Editar empleado">
                <EmpleadoEditarPage />
              </AppShell>
            }
          />
          <Route
            path="/importar"
            element={
              <AppShell activeKey="importar" title="Importar Excel">
                <ImportarPage />
              </AppShell>
            }
          />
          <Route path="*" element={<Navigate to="/empleados" replace />} />
        </Routes>
      </BrowserRouter>
    </AuthGuard>
  );
}

export default App;
