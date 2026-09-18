namespace Leads.Infrastructure.Repositories;

public sealed class AdvisorRepository(LeadsDbContext db) : IAdvisorRepository
{
    public async Task<IReadOnlyList<AdvisorDto>> ListAsync(
        long? empresaId,
        long? puntoVentaId,
        bool? activo,
        CancellationToken ct)
    {
        var query = db.Advisors.AsNoTracking().AsQueryable();

        if (empresaId.HasValue)
        {
            query = query.Where(x => x.EmpresaId == empresaId);
        }

        if (puntoVentaId.HasValue)
        {
            query = query.Where(x => x.PuntoVentaId == puntoVentaId);
        }

        if (activo.HasValue)
        {
            query = query.Where(x => x.Activo == activo);
        }

        return await query
            .OrderBy(x => x.Nombre)
            .Select(x => new AdvisorDto(
                x.AsesorId,
                x.Nombre,
                x.PuntoVentaId,
                x.EmpresaId,
                x.CapacidadDiariaLeads,
                x.Activo))
            .ToListAsync(ct);
    }
}

public sealed class MotorcycleRepository(LeadsDbContext db) : IMotorcycleRepository
{
    public async Task<IReadOnlyList<MotorcycleDto>> ListAsync(
        string? marca,
        string? segmento,
        CancellationToken ct)
    {
        var query = db.Motorcycles.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(marca))
        {
            query = query.Where(x => x.Marca == marca);
        }

        if (!string.IsNullOrWhiteSpace(segmento))
        {
            query = query.Where(x => x.Segmento == segmento);
        }

        return await query
            .OrderBy(x => x.Marca)
            .ThenBy(x => x.Linea)
            .Select(x => new MotorcycleDto(
                x.Sku,
                x.Marca,
                x.Linea,
                x.Cilindraje,
                x.Segmento,
                x.PrecioLista,
                x.PuntosVentaDisponibles,
                x.UnidadesDisponibles))
            .ToListAsync(ct);
    }
}

public sealed class PipelineRunRepository(LeadsDbContext db) : IPipelineRunRepository
{
    public async Task<IReadOnlyList<PipelineRunDto>> ListAsync(int page, int pageSize, CancellationToken ct)
    {
        var take = Math.Clamp(pageSize, 1, 100);
        var skip = Math.Max(0, page - 1) * take;

        return await db.PipelineRuns
            .AsNoTracking()
            .OrderByDescending(x => x.StartedAt)
            .Skip(skip)
            .Take(take)
            .Select(x => new PipelineRunDto(
                x.RunId,
                x.Status,
                x.StartedAt,
                x.FinishedAt,
                x.SummaryJson,
                x.Error))
            .ToListAsync(ct);
    }
}
