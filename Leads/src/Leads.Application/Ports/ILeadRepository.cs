namespace Leads.Application.Ports;

public interface ILeadRepository
{
    Task<IReadOnlyList<LeadListItem>> ListAsync(long? empresaId, long? puntoVentaId, string? status, int page, int pageSize, CancellationToken ct);
    Task<LeadDetail?> GetAsync(long leadId, CancellationToken ct);
    Task<ScoreDto?> GetScoreAsync(long leadId, CancellationToken ct);
    Task<AssignmentDto?> GetAssignmentAsync(long leadId, CancellationToken ct);
}
