namespace Leads.Api.Controllers;

[ApiController]
[Route("api/v1/pipeline")]
public sealed class PipelineController(PipelineCommands commands) : ControllerBase
{
    [HttpPost("runs")]
    public async Task<IActionResult> Start([FromBody] PipelineStartRequest request, CancellationToken ct)
    {
        var result = await commands.StartAsync(request, ct);
        return Accepted($"api/v1/pipeline/runs/{result.RunId}", result);
    }
}
