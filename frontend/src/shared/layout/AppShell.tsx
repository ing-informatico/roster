import type { ReactNode } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from 'react-oidc-context';
import { useCurrentUser } from '../../core/auth/useCurrentUser';
import { useUserRole } from '../../core/auth/useUserRole';
import { buildCognitoLogoutUrl } from '../../core/config/authConfig';
import './appShell.css';

interface NavItem {
  key: string;
  label: string;
  icon: string;
  path: string;
}

const NAV_ITEMS: NavItem[] = [
  { key: 'dashboard', label: 'Dashboard', icon: '▦', path: '/dashboard' },
  { key: 'empleados', label: 'Empleados', icon: '👤', path: '/empleados' },
  { key: 'catalogos', label: 'Catalogos', icon: '▤', path: '/catalogos' },
  { key: 'importar', label: 'Importar Excel', icon: '⭱', path: '/importar' },
  { key: 'auditoria', label: 'Auditoria', icon: '≡', path: '/auditoria' },
];

interface AppShellProps {
  activeKey: string;
  title: string;
  children: ReactNode;
}

export function AppShell({ activeKey, title, children }: AppShellProps) {
  const auth = useAuth();
  const navigate = useNavigate();
  const { displayName, greeting } = useCurrentUser();
  const { role, label: roleLabel } = useUserRole();

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
              onClick={() => navigate(item.path)}
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
            <span className={`role-badge role-${role}`}>{roleLabel}</span>
            <span className="greeting">
              {greeting}, <strong>{displayName}</strong>
            </span>
            <button
              className="logout-btn"
              onClick={() => {
                void auth.removeUser();
                window.location.href = buildCognitoLogoutUrl();
              }}
            >
              Salir
            </button>
          </div>
        </header>
        <div className="content">{children}</div>
      </div>
    </div>
  );
}
