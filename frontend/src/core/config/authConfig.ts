// Cognito OIDC configuration, fully driven by environment variables.
export const authConfig = {
  authority: import.meta.env.VITE_COGNITO_AUTHORITY as string,
  client_id: import.meta.env.VITE_COGNITO_CLIENT_ID as string,
  redirect_uri: import.meta.env.VITE_COGNITO_REDIRECT_URI as string,
  response_type: 'code',
  scope: (import.meta.env.VITE_COGNITO_SCOPE as string) ?? 'email openid phone',
};

// Cognito hosted-UI domain, used to fully sign out at the identity provider.
export const cognitoDomain = import.meta.env.VITE_COGNITO_DOMAIN as string;

// Builds the Cognito logout URL so the next login prompts for credentials again.
export function buildCognitoLogoutUrl(): string {
  const logoutUri = encodeURIComponent(authConfig.redirect_uri);
  return `https://${cognitoDomain}/logout?client_id=${authConfig.client_id}&logout_uri=${logoutUri}`;
}
