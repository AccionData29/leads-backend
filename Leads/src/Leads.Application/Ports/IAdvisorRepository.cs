namespace Leads.Application.Ports;
using Leads.Application.DTOs;
public interface IAdvisorRepository { Task<IReadOnlyList<AdvisorDto>> ListAsync(long? empresaId, long? puntoVentaId, bool? activo, CancellationToken ct); }
