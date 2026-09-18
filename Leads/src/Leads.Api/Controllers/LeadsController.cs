namespace Leads.Api.Controllers;

[ApiController]
[Route("api/v1/leads")]
public sealed class LeadsController(LeadQueries q) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> List(
        [FromQuery] long? empresaId,
        [FromQuery] long? puntoVentaId,
        [FromQuery] string? status,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 50,
        CancellationToken ct = default)
    {
        return Ok(await q.ListAsync(empresaId, puntoVentaId, status, page, pageSize, ct));
    }

    [HttpGet("{leadId:long}")]
    public async Task<IActionResult> Get(long leadId, CancellationToken ct)
    {
        var lead = await q.GetAsync(leadId, ct);
        return lead is null ? NotFound() : Ok(lead);
    }

    [HttpGet("{leadId:long}/score")]
    public async Task<IActionResult> Score(long leadId, CancellationToken ct)
    {
        var score = await q.GetScoreAsync(leadId, ct);
        return score is null ? NotFound() : Ok(score);
    }

    [HttpGet("{leadId:long}/assignment")]
    public async Task<IActionResult> Assignment(long leadId, CancellationToken ct)
    {
        var assignment = await q.GetAssignmentAsync(leadId, ct);
        return assignment is null ? NotFound() : Ok(assignment);
    }
}
