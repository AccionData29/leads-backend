namespace Leads.Application.UseCases;

public sealed class PipelineCommands(IPipelineService service)
{
    public Task<PipelineStartResponse> StartAsync(PipelineStartRequest request, CancellationToken ct)
        => service.StartAsync(request, ct);
}
