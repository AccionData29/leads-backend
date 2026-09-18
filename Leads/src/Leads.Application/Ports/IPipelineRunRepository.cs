namespace Leads.Application.Ports;

public interface IPipelineRunRepository
{
    Task<IReadOnlyList<PipelineRunDto>> ListAsync(int page, int pageSize, CancellationToken ct);
}
