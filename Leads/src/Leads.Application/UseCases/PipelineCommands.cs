namespace Leads.Application.UseCases;
using Leads.Application.DTOs; using Leads.Application.Ports;
public sealed class PipelineCommands(IPipelineService service)
{
    public Task<PipelineStartResponse> StartAsync(PipelineStartRequest request, CancellationToken ct)=>service.StartAsync(request,ct);
}
