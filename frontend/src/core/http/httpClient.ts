import axios, { AxiosError } from 'axios';
import { env } from '../config/env';

// Single shared HTTP client instance for the whole app.
export const httpClient = axios.create({
  baseURL: env.apiBaseUrl,
  headers: {
    'Content-Type': 'application/json',
  },
});

// Response interceptor: centralized error handling.
// Auth token injection will be added here when Cognito is wired up.
httpClient.interceptors.response.use(
  (response) => response,
  (error: AxiosError) => {
    // Surface a normalized error; per-feature code decides how to display it.
    return Promise.reject(error);
  },
);
