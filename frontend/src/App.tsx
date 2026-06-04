import { AppShell } from './shared/layout/AppShell';
import { EmpleadosPage } from './features/empleados/EmpleadosPage';

function App() {
  return (
    <AppShell activeKey="empleados" title="Empleados">
      <EmpleadosPage />
    </AppShell>
  );
}

export default App;
