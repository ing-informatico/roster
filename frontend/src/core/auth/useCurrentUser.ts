import { useAuth } from 'react-oidc-context';

export function useCurrentUser() {
  const auth = useAuth();
  const email = (auth.user?.profile.email as string) ?? '';
  // Derive a display name from the email local-part (e.g. john.gm -> John Gm).
  const localPart = email.split('@')[0] ?? '';
  const displayName = localPart
    .split('.')
    .filter(Boolean)
    .map((p) => p.charAt(0).toUpperCase() + p.slice(1))
    .join(' ');

  const hour = new Date().getHours();
  const greeting =
    hour < 12 ? 'Buenos dias' : hour < 19 ? 'Buenas tardes' : 'Buenas noches';

  return { email, displayName, greeting };
}
