namespace Leads.Application.Ports;

public interface IMotorcycleRepository
{
    Task<IReadOnlyList<MotorcycleDto>> ListAsync(string? marca, string? segmento, CancellationToken ct);
}
