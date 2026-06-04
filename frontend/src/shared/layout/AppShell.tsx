import type { ReactNode } from 'react';
import { useAuth } from 'react-oidc-context';
import { useCurrentUser } from '../../core/auth/useCurrentUser';
import './appShell.css';

interface NavItem {
  key: string;
  label: string;
  icon: string;
}

const NAV_ITEMS: NavItem[] = [
  { key: 'dashboard', label: 'Dashboard', icon: '▦' },
  { key: 'empleados', label: 'Empleados', icon: '👤' },
  { key: 'importar', label: 'Importar Excel', icon: '⭱' },
  { key: 'auditoria', label: 'Auditoria', icon: '≡' },
];

interface AppShellProps {
  activeKey: string;
  title: string;
  children: ReactNode;
}

export function AppShell({ activeKey, title, children }: AppShellProps) {
  const auth = useAuth();
  const { displayName, greeting } = useCurrentUser();

  return (
    <div className="shell">
      <aside className="sidebar">
        <div className="brand">
          <span className="brand-dot">R</span>
          <span>ROSTER</span>
        </div>
        <nav>
          {NAV_ITEMS.map((item) => (
            <button
              key={item.key}
              className={`nav-item${item.key === activeKey ? ' active' : ''}`}
            >
              <span className="nav-icon">{item.icon}</span>
              <span>{item.label}</span>
            </button>
          ))}
        </nav>
      </aside>

      <div className="main">
        <header className="topbar">
          <div>
            <div className="crumb">Roster · Fase 1</div>
            <h2 className="topbar-title">{title}</h2>
          </div>
          <div className="topbar-user">
            <span className="greeting">
              {greeting}, <strong>{displayName}</strong>
            </span>
            <button className="logout-btn" onClick={() => void auth.removeUser()}>
              Salir
            </button>
          </div>
        </header>
        <div className="content">{children}</div>
      </div>
    </div>
  );
}
