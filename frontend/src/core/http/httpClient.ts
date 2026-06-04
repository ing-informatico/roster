import axios, { AxiosError } from 'axios';
import { User } from 'oidc-client-ts';
import { env } from '../config/env';
import { authConfig } from '../config/authConfig';

export const httpClient = axios.create({
  baseURL: env.apiBaseUrl,
  headers: {
    'Content-Type': 'application/json',
  },
});

// Reads the current OIDC user (and its access token) from storage.
function getStoredUser(): User | null {
  const key = `oidc.user:${authConfig.authority}:${authConfig.client_id}`;
  const raw = sessionStorage.getItem(key);
  return raw ? User.fromStorageString(raw) : null;
}

// Request interceptor: attach the Cognito access token as Bearer.
httpClient.interceptors.request.use((config) => {
  const user = getStoredUser();
  if (user?.access_token) {
    config.headers.Authorization = `Bearer ${user.access_token}`;
  }
  return config;
});

// Response interceptor: centralized error handling.
httpClient.interceptors.response.use(
  (response) => response,
  (error: AxiosError) => Promise.reject(error),
);
