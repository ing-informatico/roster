# Roster — Development & Execution Instructions

**Project:** Roster (HR personnel management platform for Allied Global)
**Stack:** React 18 + TypeScript (frontend) · .NET 10 / ASP.NET Core (backend) · PostgreSQL · Amazon Cognito · Docker
**Scope owner / sole approver:** John Garzon
**Document status:** v1.1 — living document, updated as the project evolves

> This document adapts the rigor of the original "CodeRadar" rules to Roster's current architecture
> (React + .NET Core instead of Angular + Spring Boot + FastAPI). It is a guide; the rules below are
> Roster's own.

---

## 1. Core Principles

1. **Sensitive data first.** Roster stores personal and compensation data (PII). Security and privacy are primary design principles, not secondary considerations. Follow secure coding practices and avoid unsafe patterns at all times.
2. **Zero technical debt.** Any bug or issue detected is fixed immediately, without delay.
3. **No hardcoded data.** All data is obtained dynamically from the database or from proper configuration sources. No placeholder data, placeholders, or simulated responses in delivered code.
4. **No hardcoded text.** No literal strings in the UI. Every word passes through the internationalization (i18n) layer and must exist in all language JSON files.
5. **English-only codebase.** All code, variable names, comments, identifiers, and commit messages are in English. No Spanish inside code files.
6. **Everything reusable.** Chips, cards, tables, buttons, headers, titles, toggles, modals, etc. are reusable components. The whole project is refactored toward 100% reusable components, designed for future modules.
7. **Absolute respect for the prototypes.** Implement each screen exactly as shown in the approved prototype. Before creating any UI/UX element, verify it exists in the prototype and that it matches. Only John Garzon may decide to add or remove something.
8. **Never assume — verify.** Always review official documentation, existing project code, and current architecture before implementing. Validate before creating to avoid duplication.
9. **Do not alter existing functionality.** Every change preserves current behavior and system stability.
10. **Enterprise-grade and scalable.** Every component, API, and table is built for production and ready to scale, anticipating future modules.

---

## 2. Workflow Rules

1. **One step at a time.** Deliver a single step or command and wait for John's explicit confirmation before continuing to the next one.
2. **Full context before any command.** Before executing anything, briefly explain what it does, how it does it, its purpose, and where it will be added.
3. **Complete files only.** When delivering code, provide the entire file. No missing sections, no placeholders, no truncated content.
4. **Commit per validated block.** Each correct block of code that complies with the guidelines is committed and pushed. Validate compliance with this document before committing.
5. **Visual validation pointer.** After each implementation, clearly state where in the UI the change can be seen and validated.
6. **Clear committee messages.** Each successfully validated feature produces a clear, intuitive success message suitable for the committee.
7. **Deliverable context.** Every deliverable explains: what the change does, why it is required, where it is located, and where/how it can be reviewed or tested.
8. **Remember everything.** Record every implemented API, table, component, and decision so the full documentation can be regenerated at the end.

---

## 3. Frontend Standards (React 18 + TypeScript)

### Architecture & structure
- Feature-based folder structure: `core/`, `shared/`, `features/`.
- Build with Vite + TypeScript. Strong typing throughout; never use `any`.
- Functional components and custom hooks only. Keep components small and single-responsibility.
- Business logic lives in services/hooks, not in components or templates.

### State management
- Local/UI state: React hooks (`useState`, `useReducer`).
- Server state: React Query (caching, refetch, real-time sync) — no manual refresh.
- Global app state only when strictly necessary (e.g. Zustand or Context); avoid overengineering.

### Performance
- Route-based lazy loading using `React.lazy` + `Suspense`.
- `React.memo`, `useMemo`, `useCallback` to prevent unnecessary re-renders (the React equivalent of Angular's `OnPush`/`trackBy`).
- Stable, unique `key` props in lists.
- Optimized image handling; avoid complex logic in render paths.

### Reusability & UI consistency
- Shared components, hooks, utility functions, and HOCs.
- All modals, buttons, and interactive elements share the same approach, colors, and styles, and are dynamic.
- **Every input field has a tooltip** explaining clearly and intuitively what it is/does.

### Accessibility (A11y)
- Semantic HTML, ARIA attributes, full keyboard navigation.

### Security (frontend)
- Rely on React's built-in escaping; use a sanitizer (e.g. DOMPurify) when rendering HTML.
- Protection against XSS and CSRF. Always use HTTPS.
- HTTP interceptors (Axios/fetch wrappers) for auth tokens and centralized global error handling.
- Use DTOs/interfaces for all API data.

---

## 4. Backend Standards (.NET 10 / ASP.NET Core)

### Architecture
- Clean Architecture layers: Domain (entities, pure rules) -> Application (use cases, DTOs, validation) -> Infrastructure (EF Core, repositories, Excel importer) -> Api (controllers, middleware, authentication).
- Proper separation of concerns; clear naming; modular, maintainable structure.

### Data & migrations
- EF Core against PostgreSQL.
- Idempotent automatic schema management: tables and columns are created/updated from the EF Core models on application startup (via migrations applied automatically), so the schema is always in sync without manual scripts.

### Dev experience
- Use `dotnet watch` for backend hot reload — no manual backend restart.
- The frontend uses Vite HMR — no frontend downtime unless absolutely necessary.

### API rules
- Every new API endpoint is tested immediately to confirm it works as expected before commit/push.
- Server-side validation is the source of truth; client-side validation is for UX only.
- Use DTOs and interfaces; centralize error handling.

### Security (backend)
- JWT validation against Amazon Cognito (see section 6).
- Secure coding practices; avoid vulnerabilities and unsafe patterns.
- Audit logging: every data-modifying operation records who changed what and when.

---

## 5. Database (PostgreSQL)

- Single source of truth; 100% dynamic, real data. No simulated or fictitious data.
- Schema generated and evolved through EF Core migrations, applied idempotently on application startup.
- Sensitive columns (salary, bonuses) are treated as PII; role-based access control can be tightened later without modifying the model.

---

## 6. Authentication & Authorization (Amazon Cognito)

- Identity is delegated to a Cognito User Pool; the application never stores passwords.
- Login returns a signed JWT; the frontend sends it on every request; the API validates its signature, issuer, audience, and lifetime against Cognito.
- Roles are Cognito groups (e.g. RRHH-Editor, RRHH-Consulta) carried in the token; the API authorizes each endpoint by group. Authorization is enforced on the server, never only on the client.
- Cognito configuration (region, user pool id, client id) comes from environment variables.

---

## 7. Internationalization (i18n)

- 5 language JSON files, one per supported language. Pending confirmation from John: confirm the exact 5 languages.
- No hardcoded text anywhere. Every label, message, tooltip, and title is a translation key present in all language files.
- Before creating a new translation key: verify whether it already exists; reuse it if so, create it only if missing.

---

## 8. Testing

- Continuously consult the project's "Testing Rules" document throughout development.
- Every function is tested repeatedly end-to-end (n2n) until fully exhausted.
- APIs undergo unit, integration, endurance (soak), and stress/limit testing.
- Frontend: unit tests (Jest/Vitest) and end-to-end tests (Playwright/Cypress).
- Each API response is verified to match the expected contract before commit/push.
- The project must have no warnings.

---

## 9. Real-Time & Multi-Platform

- Prioritize native libraries and native APIs over browser-only workarounds.
- Use proper sync mechanisms (SSE, WebSockets) instead of manual refresh; clean up every long-lived connection.
- Pending confirmation from John: confirm whether Roster must ship for desktop and mobile, or remain web-only.

---

## 10. Engineering Conventions

- Follow clean code standards: clear naming, modular structure, maintainable architecture, separation of concerns.
- Do not use regular expressions anywhere in the code; use explicit parsing and validation libraries.
- Use the CLI and dependency tooling correctly; check for vulnerabilities (`npm audit`, `dotnet list package --vulnerable`).
- Always research how each feature can surpass current market tools.

---

## 11. Open Items Requiring John's Decision

1. The exact 5 languages for internationalization (i18n).
2. Whether Roster is web-only or must also be available for desktop/mobile.
3. Confirmation that the original cybersecurity framing is replaced by an HR/PII data protection framing.
4. Location and format of the "Testing Rules" document for this project.

---

## 12. React 18 — Full Best Practices

### Architecture & structure
- Functional components only; organize by feature (`core/`, `shared/`, `features/`); follow DRY.

### Modern React
- Custom hooks for reusable logic; JSX control flow; prefer composition over inheritance.

### State management
- Local state with hooks; server state with React Query; global state only if truly needed.

### Performance
- Lazy loading and code splitting; `React.memo`, `useMemo`, `useCallback`; stable keys.

### Clean code
- Clear naming; strong TypeScript interfaces; never `any`; logic in services/hooks.

### Security
- Keep dependencies updated; sanitize rendered HTML; protect against XSS and CSRF; always HTTPS.

### Data handling
- Centralized HTTP layer with interceptors; DTOs and interfaces for all API responses.

### Reusability, accessibility, build & testing
- Shared components, hooks, utilities; semantic HTML and ARIA; Vite/esbuild with tree shaking; unit and E2E tests.

---

## 13. .NET 10 / ASP.NET Core — Full Best Practices

### Architecture & structure
- Clean Architecture: Domain -> Application -> Infrastructure -> Api; dependencies point inward.

### Modern .NET
- Dependency injection throughout; async/await end-to-end; minimal hosting model and IOptions.

### Data & EF Core
- Code-first models; migrations applied idempotently on startup; no business logic in the database.

### Validation & contracts
- Server-side validation (FluentValidation or DataAnnotations); DTOs for every request/response; centralized error handling.

### Performance
- Async data access; pagination on every list endpoint; projection to DTOs; avoid N+1.

### Clean code
- Nullable reference types enabled; treat warnings as errors; small methods; no dead code.

### Security
- JWT bearer validation against Cognito; authorization policies by role/group; HTTPS enforced; no sensitive data in logs.

### Dev experience, testing & tooling
- `dotnet watch`; secrets via environment variables; xUnit unit tests and integration tests; `dotnet format` and analyzers.
