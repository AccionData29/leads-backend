namespace Leads.Application.UseCases;

public sealed class ReferenceQueries(IAdvisorRepository advisors, IMotorcycleRepository motorcycles, IPipelineRunRepository runs)
{
    public Task<IReadOnlyList<AdvisorDto>> AdvisorsAsync(long? e, long? p, bool? a, CancellationToken ct)
        => advisors.ListAsync(e, p, a, ct);

    public Task<IReadOnlyList<MotorcycleDto>> MotorcyclesAsync(string? m, string? s, CancellationToken ct)
        => motorcycles.ListAsync(m, s, ct);

    public Task<IReadOnlyList<PipelineRunDto>> RunsAsync(int page, int size, CancellationToken ct)
        => runs.ListAsync(page, size, ct);
}
