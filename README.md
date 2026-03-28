# Criosho Admin

## Etapa 1 (Backend Base)

Se inicializó una base de backend en capas con .NET 8:

- `Criosho.Admin.API`
- `Criosho.Admin.Domain`
- `Criosho.Admin.DTOs`
- `Criosho.Admin.Services`
- `Criosho.Admin.Repository`
- `Criosho.Admin.Clients`

Incluye:

- Identity Core con EF Core Code First
- JWT access token + refresh token
- DI por extensiones
- Migraciones automáticas al iniciar (`Database.Migrate()`)
- Respuesta estándar para APIs
- Endpoints iniciales: `auth/login`, `auth/refresh-token`, `health`

## Próxima etapa sugerida

- Alta de primer módulo funcional ABM (por ejemplo: Roles y Usuarios)
- Implementación de confirmación de email y recuperación de contraseña
- Scaffold Angular con layout autenticado y módulo `auth`
