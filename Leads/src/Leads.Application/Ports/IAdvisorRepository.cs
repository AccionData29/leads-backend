namespace Leads.Application.Ports;

public interface IAdvisorRepository
{
    Task<IReadOnlyList<AdvisorDto>> ListAsync(long? empresaId, long? puntoVentaId, bool? activo, CancellationToken ct);
}
