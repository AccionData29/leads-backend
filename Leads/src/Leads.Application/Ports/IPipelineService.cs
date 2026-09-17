namespace Leads.Application.Ports;
using Leads.Application.DTOs;
public interface IPipelineService
{
    Task<PipelineStartResponse> StartAsync(PipelineStartRequest request, CancellationToken ct);
}
