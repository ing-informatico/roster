import type { ReactNode } from 'react';
import { useAuth } from 'react-oidc-context';
import './authGuard.css';

interface AuthGuardProps {
  children: ReactNode;
}

export function AuthGuard({ children }: AuthGuardProps) {
  const auth = useAuth();

  if (auth.isLoading) {
    return <div className="auth-screen">Cargando...</div>;
  }

  if (auth.error) {
    return <div className="auth-screen">Error de autenticacion: {auth.error.message}</div>;
  }

  if (!auth.isAuthenticated) {
    return (
      <div className="auth-screen">
        <div className="auth-card">
          <div className="auth-brand">
            <span className="auth-dot">R</span>
            <span>ROSTER</span>
          </div>
          <h1>Iniciar sesion</h1>
          <p>Accede al panel de Roster con tu cuenta corporativa.</p>
          <button className="auth-btn" onClick={() => void auth.signinRedirect()}>
            Iniciar sesion
          </button>
        </div>
      </div>
    );
  }

  return <>{children}</>;
}
