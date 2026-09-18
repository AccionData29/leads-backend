namespace Leads.Application.UseCases;

public sealed class LeadQueries(ILeadRepository repo)
{
    public Task<IReadOnlyList<LeadListItem>> ListAsync(
        long? empresaId,
        long? puntoVentaId,
        string? status,
        int page,
        int pageSize,
        CancellationToken ct)
        => repo.ListAsync(empresaId, puntoVentaId, status, page, pageSize, ct);

    public Task<LeadDetail?> GetAsync(long id, CancellationToken ct) => repo.GetAsync(id, ct);

    public Task<ScoreDto?> GetScoreAsync(long id, CancellationToken ct) => repo.GetScoreAsync(id, ct);

    public Task<AssignmentDto?> GetAssignmentAsync(long id, CancellationToken ct) => repo.GetAssignmentAsync(id, ct);
}
