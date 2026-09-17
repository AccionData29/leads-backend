# Contrato funcional Leads Backend ↔ Python Pipeline

## Componentes

- `leads-backend`: API .NET 8, dominio operacional y migraciones EF Core.
- `pipeline_build`: worker/API Python para ingesta, normalización, deduplicación, IA, scoring y asignación.
- PostgreSQL: persistencia compartida.

## Flujo

1. Angular consume `leads-backend`.
2. .NET consulta PostgreSQL para leads, score, enriquecimiento, asignación y catálogos.
3. .NET puede iniciar una ejecución mediante `POST /api/v1/pipeline/runs`.
4. .NET llama al servicio interno Python `POST /api/v1/pipeline/runs`.
5. Python crea `pipeline_runs` en estado `RUNNING` y procesa de forma asíncrona.
6. Python hace upsert de datos base y agrega históricos de enriquecimiento, score y asignación.
7. .NET expone el estado de ejecución mediante `GET /api/v1/pipeline-runs`.

## Endpoints .NET

- `GET /api/v1/health`
- `GET /api/v1/leads`
- `GET /api/v1/leads/{leadId}`
- `GET /api/v1/leads/{leadId}/score`
- `GET /api/v1/leads/{leadId}/assignment`
- `GET /api/v1/advisors`
- `GET /api/v1/motorcycles`
- `GET /api/v1/pipeline-runs`
- `POST /api/v1/pipeline/runs`

El detalle del lead devuelve además cliente, último enriquecimiento, último score, última asignación y datos de matching de motocicleta.

## Endpoint Python interno

- `GET /api/v1/health`
- `POST /api/v1/pipeline/runs`

Request:

```json
{ "source_dir": null }
```

Response `202`:

```json
{
  "run_id": "uuid",
  "status": "RUNNING"
}
```

## Propiedad de datos

### .NET / EF Core

- Es dueño del esquema productivo y las migraciones.
- Expone el contrato HTTP consumido por Angular.
- No contiene lógica de IA/ML del pipeline.

### Python

- Es dueño del procesamiento del pipeline.
- Puede insertar/actualizar los datos producidos por el pipeline mediante SQL idempotente.
- No administra migraciones productivas.

### PostgreSQL

Tablas compartidas:

- `leads.customers`
- `leads.leads`
- `leads.motorcycles`
- `leads.advisors`
- `leads.conversations`
- `leads.messages`
- `leads.lead_enrichments`
- `leads.lead_scores`
- `leads.lead_assignments`
- `leads.pipeline_runs`

## Idempotencia

- Leads, clientes, motos y asesores utilizan upsert por clave natural/primaria.
- Conversaciones utilizan `conversacion_id` único.
- Enriquecimientos, scores y asignaciones son históricos; cada ejecución puede generar una nueva versión.
- `pipeline_runs.run_id` identifica de forma única una ejecución.

## Identificador de cliente

`customer_id` es UUID determinístico generado desde la clave canónica del cliente. Esto evita el desacople de tipos entre Python y PostgreSQL/EF Core.

## Migraciones

Producción:

```bash
dotnet ef database update --project src/Leads.Infrastructure --startup-project src/Leads.Api
```

El `schema.sql` de Python es únicamente bootstrap local/assessment y está alineado al esquema EF Core.
