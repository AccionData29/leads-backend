namespace Leads.Application.Ports;
using Leads.Application.DTOs;
public interface IMotorcycleRepository { Task<IReadOnlyList<MotorcycleDto>> ListAsync(string? marca, string? segmento, CancellationToken ct); }
