# Leads Backend

Backend .NET 8 para la plataforma de gestión y priorización de leads de motocicletas.

## Arquitectura

Basado en Hexagonal Architecture / Ports & Adapters, adaptada a un monolito modular:

- `Leads.Domain`: dominio puro, sin dependencias de EF Core ni ASP.NET.
- `Leads.Application`: casos de uso y puertos.
- `Leads.Infrastructure`: PostgreSQL + EF Core y adaptadores de persistencia.
- `Leads.Api`: adaptador HTTP, DI, Swagger y health checks.

La referencia arquitectónica usada indica que la arquitectura hexagonal separa Application, Domain e Infrastructure mediante puertos/adaptadores y está pensada también para aplicaciones monolíticas. citeturn0search0

## Responsabilidades

Python (`leads-pipeline`) procesa ingesta, normalización, deduplicación, IA, scoring y asignación. Este backend expone y administra el dominio operacional para Angular y persiste el resultado en PostgreSQL.

Las migraciones productivas son propiedad de este repositorio mediante EF Core. No se ejecutan automáticamente al arrancar la API en producción.

## Proyectos

```text
src/
  Leads.Domain/
  Leads.Application/
  Leads.Infrastructure/
  Leads.Api/
tests/
  Leads.UnitTests/
```

## Endpoints iniciales

- `GET /api/v1/health`
- `GET /api/v1/leads`
- `GET /api/v1/leads/{leadId}`
- `GET /api/v1/leads/{leadId}/score`
- `GET /api/v1/leads/{leadId}/assignment`
- `GET /api/v1/advisors`
- `GET /api/v1/motorcycles`
- `GET /api/v1/pipeline-runs`

## Ejecutar

Requisitos: .NET 8 SDK y PostgreSQL 15+.

```bash
dotnet restore
dotnet build
dotnet test
```

Configurar `ConnectionStrings__Default` o `DATABASE_URL`.

Para aplicar migraciones explícitamente:

```bash
dotnet ef database update --project src/Leads.Infrastructure --startup-project src/Leads.Api
```

## Nota de validación

El entorno usado para generar este entregable no dispone del SDK `dotnet`, por lo que el código no pudo ser compilado aquí. La estructura, referencias, namespaces y contratos fueron generados para .NET 8 + EF Core 8 y deben validarse en CI con el SDK instalado.

## Integración funcional con Python

La API puede iniciar el pipeline mediante `POST /api/v1/pipeline/runs`. El adaptador `PipelineHttpClient` llama al servicio Python configurado en `Pipeline:BaseUrl` (por defecto `http://localhost:8001`).

En Docker Compose se levantan PostgreSQL, Python Pipeline API y .NET API en la misma red. Primero aplicar la migración EF Core y después consumir Swagger en `/swagger`.
