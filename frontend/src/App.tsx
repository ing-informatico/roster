import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import { AuthGuard } from './core/auth/AuthGuard';
import { AppShell } from './shared/layout/AppShell';
import { EmpleadosPage } from './features/empleados/EmpleadosPage';
import { EmpleadoFichaPage } from './features/empleados/EmpleadoFichaPage';

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
          <Route path="*" element={<Navigate to="/empleados" replace />} />
        </Routes>
      </BrowserRouter>
    </AuthGuard>
  );
}

export default App;
