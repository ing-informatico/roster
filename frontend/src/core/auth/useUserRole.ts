import { useAuth } from 'react-oidc-context';

export type UserRole = 'editor' | 'consulta' | 'unknown';

const EDITOR_GROUP = 'RRHH-Editor';
const CONSULTA_GROUP = 'RRHH-Consulta';

interface CognitoProfile {
  'cognito:groups'?: string[];
}

export function useUserRole() {
  const auth = useAuth();
  const profile = auth.user?.profile as CognitoProfile | undefined;
  const groups = profile?.['cognito:groups'] ?? [];

  const role: UserRole = groups.includes(EDITOR_GROUP)
    ? 'editor'
    : groups.includes(CONSULTA_GROUP)
      ? 'consulta'
      : 'unknown';

  return {
    role,
    isEditor: role === 'editor',
    isConsulta: role === 'consulta',
    label: role === 'editor' ? 'Editor' : role === 'consulta' ? 'Solo lectura' : 'Sin rol',
  };
}
