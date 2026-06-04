import { AuthGuard } from './core/auth/AuthGuard';
import { AppShell } from './shared/layout/AppShell';
import { EmpleadosPage } from './features/empleados/EmpleadosPage';

function App() {
  return (
    <AuthGuard>
      <AppShell activeKey="empleados" title="Empleados">
        <EmpleadosPage />
      </AppShell>
    </AuthGuard>
  );
}

export default App;
