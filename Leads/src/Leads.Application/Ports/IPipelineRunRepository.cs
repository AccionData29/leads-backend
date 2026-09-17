namespace Leads.Application.Ports;
using Leads.Application.DTOs;
public interface IPipelineRunRepository { Task<IReadOnlyList<PipelineRunDto>> ListAsync(int page, int pageSize, CancellationToken ct); }
