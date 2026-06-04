// Cognito OIDC configuration, fully driven by environment variables.
// To migrate to another Cognito user pool, only the .env values change.
export const authConfig = {
  authority: import.meta.env.VITE_COGNITO_AUTHORITY as string,
  client_id: import.meta.env.VITE_COGNITO_CLIENT_ID as string,
  redirect_uri: import.meta.env.VITE_COGNITO_REDIRECT_URI as string,
  response_type: 'code',
  scope: (import.meta.env.VITE_COGNITO_SCOPE as string) ?? 'email openid phone',
};
