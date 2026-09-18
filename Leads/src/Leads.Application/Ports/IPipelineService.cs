namespace Leads.Application.Ports;

public interface IPipelineService
{
    Task<PipelineStartResponse> StartAsync(PipelineStartRequest request, CancellationToken ct);
}
